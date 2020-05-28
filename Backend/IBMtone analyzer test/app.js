
'use strict';

require('dotenv').config({silent: true});

const express = require('express');
const app = express();
const ToneAnalyzerV3 = require('ibm-watson/tone-analyzer/v3');
const { IamAuthenticator } = require('ibm-watson/auth');

const toneAnalyzer = new ToneAnalyzerV3({
  authenticator: new IamAuthenticator({
    apikey: process.env.TONE_ANALYZER_IAM_APIKEY || 'type-key-here',
  }),
  url: process.env.TONE_ANALYZER_URL,
});

// Bootstrap application settings
require('./config/express')(app);

app.get('/', function(req, res) {
  res.render('index');
});

app.post('/api/tone', async function(req, res, next) {
  try {
    const { result } = await toneAnalyzer.tone(req.body);
    res.json(result);
  } catch (error) {
    next(error);
  }
});

// error-handler application settings
require('./config/error-handler')(app);

module.exports = app;
