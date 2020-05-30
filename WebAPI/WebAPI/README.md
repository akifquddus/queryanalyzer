# Google Cloud Natural language API

Google Cloud Natural language API is used in this project with the objective of applying sentiment analysis of text documents.

## Setting permission

You need to acces Google Cloud Platform and generate your private json key. (https://cloud.google.com/natural-language/docs/quickstart).
After generating this Json key you should go to Startup.cs in WebAPI folder and include the address for the key.


## Controllers

We have the UserProfileController and ApplicationUserController responsible for the log in and user management controller. 
The GoogleDataController is responsible for calls in the google API. So far only the get verb is implemented with a default message. 
In future work the message will be present in the body of the heml. Other verbs will follow the same principle.
