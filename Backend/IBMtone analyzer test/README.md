

The IBM Watson Tone Analyzer service is natural language processing API to classify 7 tones of written tex like :anger, fear, joy, sadness, confident, analytical, and tentative.
it can detect in content level and sentence level. 

In order to run project please follow the 
1. Sign up for an [IBM Cloud account](https://cloud.ibm.com/registration/).
1. Download the [IBM Cloud CLI](https://cloud.ibm.com/docs/cli/index.html#overview).
1. Create an instance of the Tone Analyzer service and get your credentials:
    - Go to the [Tone Analyzer](https://cloud.ibm.com/catalog/services/tone-analyzer) page in the IBM Cloud Catalog.
    - Log in to your IBM Cloud account.
    - Click **Create**.
    - Click **Show** to view the service credentials.
    - Copy the `apikey` value.
    - Copy the `url` value.

## Configuring the application

1. In the application folder, copy the *.env.example* file and create a file called *.env*

    ```
    cp .env.example .env
    ```

2. Open the *.env* file and add the service credentials that you obtained in the previous step.

    Example *.env* file that configures the `apikey` and `url` for a Tone Analyzer service instance:

    ```
    TONE_ANALYZER_IAM_APIKEY=X4rbi8vwZmKpXfowaS3GAsA7vdy17Qh7km5D6EzKLHL2
    TONE_ANALYZER_URL=https://gateway-wdc.watsonplatform.net/tone-analyzer/api
    ```
    ```
    
    
SDK managing the IAM token. 
```
const ToneAnalyzerV3 = require('ibm-watson/tone-analyzer/v3');
const { IamAuthenticator } = require('ibm-watson/auth');

const toneAnalyzer = new ToneAnalyzerV3({
  version: '{version}',
  authenticator: new IamAuthenticator({
    apikey: '{apikey}',
  }),
  url: 'https://api.eu-de.tone-analyzer.watson.cloud.ibm.com',
});
```


## Running locally


1. Install the dependencies

    ```
    npm install
    ```

1. Run the application

    ```
    npm start
    ```
  
