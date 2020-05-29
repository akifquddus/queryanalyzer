from http.server import BaseHTTPRequestHandler, HTTPServer
import time
import tweepy
from textblob import TextBlob
import csv
import re
import sys
import urllib
import pandas as pd
from sklearn.feature_extraction.text import CountVectorizer
import numpy as np
from sklearn import linear_model
from sklearn.linear_model import Perceptron
from sklearn.preprocessing import PolynomialFeatures
from sklearn.svm import SVC
from sklearn.naive_bayes import GaussianNB
from rake_nltk import Rake
import nltk
import math

hostName = "localhost"
hostPort = 9000

def senti(noOfSearchTerms,topicname):

    consumer_key='g6hWMVoCGEWaYDWg3Km3YaehA'
    consumer_secret='KqMmBhdAsSRcBTO7w18hzBYm4G4BgbfWYHc7lfSmPDUvbCBh4U'

    access_token_key='582955639-7fbAiHMKNz4Mizm26Jbcp0yX9mzMa9GhEyYhoXb3'
    access_token_secret='QoQT7WIZVuQ3HD7ipSuA7MxvOxcHA94suGiOeFvVBXE5x'

    auth=tweepy.OAuthHandler(consumer_key,consumer_secret)
    auth.set_access_token(access_token_key,access_token_secret)

    api=tweepy.API(auth)
    noOfSearchTerms=noOfSearchTerms
    topicname=topicname
    h1=noOfSearchTerms
    noOfSearchTerms = noOfSearchTerms  + 1000    ### 500 extra tweet so the data can be processed

    tweets = tweepy.Cursor(api.search, q=topicname, lang="en").items(noOfSearchTerms)

    unwanted_words = ['@','RT',':','https','http']
    symbols = ['@','#']
    data=[]
    url = {}
    n1=h1
    pp1=0
    pp2=0
    pp3=0
    s1=0
    f=0
    mmm=0
    n=[]
    pos=[]
    neg=[]
    neu=[]
    posneg=[]
    r=0
    total=[]
    ttest=[]
    urls=[]
    times=[]
    for tweet in tweets:
        z1=0
        if(s1==h1+1):           #for the tweets the user have enter
            break
        time = tweet.created_at
        url = 'https://twitter.com/statuses/' + tweet.id_str
        text=tweet.text
        textWords=text.split()
        u=0
        cleanedTweet=' '.join(re.sub("(@[A-Za-z0-9]+)|([^0-9A-Za-z \t])|(\w+:\/\/\S+)|(RT)", " ", text).split())
        if(f==1):
            while(z1 <s1):      ##   for the retweets
                if(n[z1] == cleanedTweet):
                    z1=z1+1
                    u=1
                else:
                    z1=z1+1
        r=0
        if (len(cleanedTweet.split()) > 5 and u==0):
            analysis= TextBlob(cleanedTweet)
            polarity = 'Positive'
            total.append(cleanedTweet)
            if(analysis.sentiment.polarity < 0):
                polarity = 'Negative'
                n1=n1-1
                pp2=pp2+1
                neg.append(cleanedTweet)
                posneg.append(cleanedTweet)
                ttest.append(3)
                mmm=mmm+1
            elif(0<=analysis.sentiment.polarity <=0.2):
                polarity = 'Neutral'
                n1=n1-1
                pp1=pp1+1
                neu.append(cleanedTweet)
                posneg.append(cleanedTweet)
                ttest.append(4)
                mmm=mmm+1
            else:
                pos.append(cleanedTweet)
                posneg.append(cleanedTweet)
                ttest.append(2)
                pp3=pp3+1
                mmm=mmm+1
            s2=cleanedTweet
            s1=s1+1
            dic={}
            dic['Sentiment']=polarity
            dic['Tweet']=cleanedTweet
            dic['URL']= url
            dic['Time']= time
            dic['Sentiment Scores']=analysis.sentiment
            data.append(dic)
            urls.append(url)
            times.append(time)

            df=pd.DataFrame(data)
            df.to_csv('analysis.csv')
            f=1
            n.append(cleanedTweet)
        else:
            salu=500
    print("number of positive tweets are ",pp3)
    print("number of neutral tweets are ",pp1)
    print("number of negative tweets are ",pp2)



    ###bag of words

    vectorizer = CountVectorizer()
    salman = vectorizer.fit_transform(posneg)
    zzz=salman.toarray()
    

    ###classfiers for making xtrain ytrain xtest 


    count=0
    xtest=[]
    xneu=[]
    xtrain=[]
    ytrain=[]
    ytest=[]
    ytest1=[]
    ytest2=[]
    while(count<len(ttest)):
        if(ttest[count]==2):
            xtrain.append(zzz[count])
            ytrain.append(2)
            count=count+1
        elif(ttest[count]==3):
            xtrain.append(zzz[count])
            ytrain.append(3)
            count=count+1
        elif(ttest[count]==4):
            xneu.append(zzz[count])
            count=count+1


    ###logrog
                       
    xtrain1 = np.array(xtrain)
    clf = linear_model.SGDClassifier(max_iter = 1000,shuffle=False,loss='log')
    clf.fit(xtrain1,ytrain)
    value=0
    while(value<len(xneu)):
        um=xneu[value]
        a=clf.predict([um])
        aa=clf.predict_log_proba([um])
        hh=aa[0]
        hhh=hh[1]
        hhhh=hh[0]
        q11=math.exp(hhh)
        q12=math.exp(hhhh)
        if(q11<q12):
            mm=q12-q11
        else:
            mm=q11-q12
        if(mm<0.1):
            ytest.append([4])
        else:
            ytest.append(a)
        value=value+1



    #print("log")

    #print(len(ytest))
    #print(ytest)


    ### svm 


    xtrain2 = np.array(xtrain)
    clf1 = SVC(kernel='linear',probability= True)
    clf1.fit(xtrain2,ytrain) 
    value=0
    while(value<len(xneu)):
        um=xneu[value]
        a=clf1.predict([um])
        aa=clf.predict_log_proba([um])
        hh=aa[0]
        hhh=hh[1]
        hhhh=hh[0]
        q11=math.exp(hhh)
        q12=math.exp(hhhh)
        if(q11<q12):
            mm=q12-q11
        else:
            mm=q11-q12
        if(mm<0.1):
            ytest1.append([4])
        else:
            ytest1.append(a)
        value=value+1
            
    #print("svm")

    #print(len(ytest1))
    #print(ytest1)


    ###naive bayes


    xtrain3 = np.array(xtrain)
    clf2 = GaussianNB()
    clf2.fit(xtrain3,ytrain)
    value=0
    while(value<len(xneu)):
        um=xneu[value]
        a=clf2.predict([um])
        ytest2.append(a)
        value=value+1

    #print(" nb values")
    #print(ytest2)
    #print(len(ytest2))




    ##for all classifier if two values are same select those classifier


    finaltest=[]
    i=0
    length=len(ytest)
    while(i<length):
        if(ytest[i]==ytest1[i] and ytest[i]==ytest2[i] and ytest1[i]==ytest2[i]):
            finaltest.append(ytest[i])
        elif(ytest[i]==ytest1[i] and ytest[i]!=ytest2[i] and ytest1[i]!=ytest2[i]):
            finaltest.append(ytest[i])
        elif(ytest[i]!=ytest1[i] and ytest[i]==ytest2[i] and ytest1[i]!=ytest2[i]):
            finaltest.append(ytest[i])
        elif(ytest[i]!=ytest1[i] and ytest[i]!=ytest2[i] and ytest1[i]==ytest2[i]):
            finaltest.append(ytest1[i])
        else:
            yyyyy=787878
        i=i+1
            
        
   # print("finaltest")
   # print(finaltest)
   # print(len(finaltest))


    ### after classifier the results

    ff1=0
    ff2=0
    ff3=0
    qq=0
    numb=0
    dic={}
    data=[]
    posafter=[]
    negafter=[]
    while(numb<len(n)):
        cleanedTweet=n[numb]
        url=urls[numb]
        time=times[numb]
        analysis= TextBlob(cleanedTweet)
        polarity = 'Positive'
        if(analysis.sentiment.polarity < 0):
            polarity = 'Negitive'
            ff1=ff1+1
            negafter.append(cleanedTweet)
        elif(0<=analysis.sentiment.polarity <=0.2):
            if(finaltest[qq]==[4]):
                polarity = 'neutral'
                ff3=ff3+1
            elif(finaltest[qq]==3):
                polarity = 'Neg'
                ff1=ff1+1
                negafter.append(cleanedTweet)
            elif(finaltest[qq]==2):
                polarity = 'pos'
                ff2=ff2+1
                posafter.append(cleanedTweet)
                
            qq=qq+1

        else:
            ff2=ff2+1
            posafter.append(cleanedTweet)
        numb=numb+1
        dic={}
        dic['Sentiment']=polarity
        dic['Tweet']=cleanedTweet
        dic['URL']= url
        dic['Time']= time
        dic['Sentiment Scores']=analysis.sentiment
        data.append(dic)
        df=pd.DataFrame(data)
        df.to_csv('analysis2.csv')
    print("after claffication")   
    print("postive tweets are",ff2)
    print("negative tweets are",ff1)
    print("neutral tweets are",ff3)
    i=0
    text1=''

    



    
    while(i<len(posafter)):
        text1=text1+posafter[i]
        text1=text1+". "
        i=i+1

    i=0
    text=''
    while(i<len(negafter)):
        text=text+negafter[i]
        text=text+". "
        i=i+1



    #print("positive tweets text")
    #print(text1)
    #print("negtive tweets text")
    #print(text)


    # Used when tokenizing words
    sentence_re = r'''(?x)          # set flag to allow verbose regexps
            (?:[A-Z]\.)+        # abbreviations, e.g. U.S.A.
          | \w+(?:-\w+)*        # words with optional internal hyphens
          | \$?\d+(?:\.\d+)?%?  # currency and percentages, e.g. $12.40, 82%
          | \.\.\.              # ellipsis
          | [][.,;"'?():_`-]    # these are separate tokens; includes ], [
        '''

    lemmatizer = nltk.WordNetLemmatizer()
    stemmer = nltk.stem.porter.PorterStemmer()

    #Taken from Su Nam Kim Paper...
    grammar = r"""
        NBAR:
            {<NN.*|JJ>*<NN.*>}  # Nouns and Adjectives, terminated with Nouns
            
        NP:
            {<NBAR>}
            {<NBAR><IN><NBAR>}  # Above, connected with in/of/etc...
    """

    #negative tweets grammer
    chunker = nltk.RegexpParser(grammar)


    toks = nltk.regexp_tokenize(text, sentence_re)
    postoks = nltk.tag.pos_tag(toks)

 #   print(postoks)


    tree = chunker.parse(postoks)



    from nltk.corpus import stopwords
    stopwords = stopwords.words('english')


    def leaves(tree):
        """Finds NP (nounphrase) leaf nodes of a chunk tree."""
        for subtree in tree.subtrees(filter=lambda t: t.label() == 'NP'): #for subtree in tree.subtrees(filter = lambda t: t.node=='NP'):
            yield subtree.leaves()

    def normalise(word):
        """Normalises words to lowercase and stems and lemmatizes it."""
        word = word.lower()
        word = stemmer.stem(word)#_word(word)
        word = lemmatizer.lemmatize(word)
        return word

    def acceptable_word(word):
        """Checks conditions for acceptable word: length, stopword."""
        accepted = bool(2 <= len(word) <= 40
            and word.lower() not in stopwords)
        return accepted


    def get_terms(tree):
        for leaf in leaves(tree):
            term = [ normalise(w) for w,t in leaf if acceptable_word(w) ]
            yield term

    terms = get_terms(tree)
    '''
    for term in terms:
        for word in term:
            print(word+' ')
        print('')
    '''
    #postive tweets grammer
    chunker1 = nltk.RegexpParser(grammar)

    toks1 = nltk.regexp_tokenize(text1, sentence_re)
    postoks1 = nltk.tag.pos_tag(toks1)
    #print("positive grammer")
    #print(postoks1)

    tree1 = chunker.parse(postoks1)

    terms1 = get_terms(tree1)
    '''
    for term in terms1:
        for word in term:
            print(word+' ')
        print('')
    '''


    #rake for keywords for negative tweets

    r=Rake()

 #   print("cons scores")
    r.extract_keywords_from_text(text)
    consscores=r.get_ranked_phrases_with_scores()

 #   print(consscores)

    p1=len(consscores)
    p2=len(consscores[0])
    freshcons=[]
    i=0
    while(i<p1):
        p3=consscores[i][1]
        p4=consscores[i][0]
        h=p3.split()
        
        if(len(h)<=3 and p4!=1.0):
            freshcons.append(p3)
        i=i+1
            
#    print(freshcons)




    #rake for keywords for positive tweets

#    print("pros scores")
    r.extract_keywords_from_text(text1)
    proscores=r.get_ranked_phrases_with_scores()

    #print(proscores)

    p1=len(proscores)
    p2=len(proscores[0])
    freshpros=[]
    i=0
    while(i<p1):
        p3=proscores[i][1]
        p4=proscores[i][0]
        h=p3.split()
        if(len(h)<=3 and p4!=1.0):
            freshpros.append(p3)
        i=i+1
            
    #print(freshpros)

    ##for converting both grammers to lower case

    yyy=[[x.lower() for x in line] for line in postoks]
    yyy1=[[x.lower() for x in line] for line in postoks1]


    #rules select grammer==nnp and keyword for negative



    i=0
    y=0
    conlist=[]
    p=0
    while(i<len(freshcons)):
        h=freshcons[i].split()
        y=0
        while(y<len(h)):
            if(h[y] == topicname ):
                conlist.append(freshcons[i])
                y=y+1
                break
            else:
                y=y+1
        i=i+1
    k=0
    while(k<len(freshcons)):
        h=freshcons[k].split()
        p=0
        while(p<len(yyy)):
            if h[0] in yyy[p]:
                if(len(h)==3):
                     if(yyy[p][1]=='nnp' or yyy[p+1][1]=='nnp' or yyy[p+2][1]=='nnp'):
                         conlist.append(freshcons[k])
                         break
                elif(len(h)==2):
                    if(yyy[p][1]=='nnp' or yyy[p+1][1]=='nnp'):
                         conlist.append(freshcons[k])
                         break
                elif(len(h)==1):
                     if(yyy[p][1]=='nnp'):
                         conlist.append(freshcons[k])
                         break
                else:
                    pppp=555555
                p=p+1
            else:
                p=p+1
        k=k+1
                
    #print("negative tweets key words")
    #print(freshcons)
    print("conlist")
    print(conlist)



    #rules select grammer==nnp and keyword for positive


    i=0
    y=0
    prolist=[]
    p=0
    while(i<len(freshpros)):
        h=freshpros[i].split()
        y=0
        while(y<len(h)):
            if(h[y] == topicname ):
                prolist.append(freshpros[i])
                y=y+1
                break
            else:
                y=y+1
        i=i+1
    k=0
    while(k<len(freshpros)):
        h=freshpros[k].split()
        p=0
        while(p<len(yyy1)):
            if h[0] in yyy1[p]:
                if(len(h)==3):
                     if(yyy1[p][1]=='nnp' or yyy1[p+1][1]=='nnp' or yyy1[p+2][1]=='nnp'):
                         prolist.append(freshpros[k])
                         break
                elif(len(h)==2):
                    if(yyy1[p][1]=='nnp' or yyy1[p+1][1]=='nnp'):
                         prolist.append(freshpros[k])
                         break
                elif(len(h)==1):
                     if(yyy1[p][1]=='nnp'):
                         prolist.append(freshpros[k])
                         break
                else:
                    pppp=555555
                p=p+1
            else:
                p=p+1
        k=k+1

      

    
    #print("positive tweets key words")
    #print(freshpros)

        print("prolist")
        print(prolist)
         
        fh = open("output.txt","w+")
        pros = open("proslist.txt","w+")
        cons = open("conslist.txt","w+")
        fh.write( str(pp3) )
        fh.write( "\n"+str(pp2) )
        fh.write( "\n"+str(pp1) )
        fh.write( "\n"+str(ff2) )
        fh.write( "\n"+str(ff1) )
        fh.write( "\n"+str(ff3) )
        cons.write( "\n"+str(conlist) )
        pros.write( "\n"+str(prolist) )
        fh.close()
        pros.close()
        cons.close()

       # myAPI = "http://localhost/Twitter/set_return.php?PP3="+str(pp3)+"&PP2="+str(pp2)+"&PP1="+str(pp1)+"&ff2="+str(ff2)+"&ff1="+str(ff1)+"&ff3="+str(ff3)+"&conlist="+str(conlist)+"&prolist="+str(prolist)

       # print(myAPI)

    return pp3,pp2,pp1,ff2,ff1,ff3,conlist,prolist


class MyServer(BaseHTTPRequestHandler):
    def do_GET(self):
        input = self.path
        a = input.split("?")
        b = a[1].split("&")
        text = b[0].split("=")
        num = b[1].split("=")
        '''
        output = num[1]+ " "+text[1]
        '''
        n= int(num[1])
        t= text[1]

        


        a=senti(n,t)
      #  output = "Redirecting"


        
        self.send_response(200)
        self.send_header("Content-type", "text/html")
        self.end_headers()
        self.wfile.write(bytes("<html><head><title>Title goes here.</title></head>", "utf-8"))
        self.wfile.write(bytes("<body><button><a href='http://localhost/tests/test.php'>Results</button>", "utf-8"))
       # self.wfile.write(bytes("<p>You Send path: %s</p>" % output, "utf-8"))
        self.wfile.write(bytes("</body></html>", "utf-8"))

        print(self.path)

myServer = HTTPServer((hostName, hostPort), MyServer)
print(time.asctime(), "Server Starts - %s:%s" % (hostName, hostPort))

try:
    myServer.serve_forever()
except KeyboardInterrupt:
    pass

myServer.server_close()
print(time.asctime(), "Server Stops - %s:%s" % (hostName, hostPort))

