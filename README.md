### This is a repository for the UCL Neuroscience/Machine Learning coding exercise.


##### Exercise

Replicate Figure 3.7 from

@book{bishop2006pattern,
  title={Pattern recognition and machine learning},
  author={Bishop, Christopher M and Nasrabadi, Nasser M},
  volume={4},
  number={4},
  year={2006},
  url={https://eur01.safelinks.protection.outlook.com/?url=https%3A%2F%2Fwww.microsoft.com%2Fen-us%2Fresearch%2Fuploads%2Fprod%2F2006%2F01%2FBishop-Pattern-Recognition-and-Machine-Learning-2006.pdf&data=05%7C01%7Cm.sainsbury%40ucl.ac.uk%7C0d446365241340ac01fd08dbc0d423e3%7C1faf88fea9984c5b93c9210a11d9a5c2%7C0%7C0%7C638315789589566679%7CUnknown%7CTWFpbGZsb3d8eyJWIjoiMC4wLjAwMDAiLCJQIjoiV2luMzIiLCJBTiI6Ik1haWwiLCJXVCI6Mn0%3D%7C3000%7C%7C%7C&sdata=ZfZzrrH7zhT5N2fuA5g0oZB%2BEnEI5qGt78A8UnV9%2FVw%3D&reserved=0},                                                                                                                                                    
  publisher={Springer}
} 

a) in Python,
b) in C#,
c) in Bonsai.

For personal reference, the figure is on page 155.


##### Overview

The exercise is about implementing a Bayesian model within the framework of linear regression.


##### Problem Description

Maximizing likelihood inherently drives the addition of more parameters to the model to boost accuracy. This leads to increased model complexity, which may compromise the models ability to generalize to new data. This is the problem of overfitting.

Regularization techniques can help to offset the problem of overfitting by penalizing superfluous parameters, but their efficacy depends on the specific regularization employed, since certain basis functions can skew the results towards bias or variance.

In contrast, the Bayesian approach makes use of a prior distribution of expected model parameters. The expected probabilities then update through subsequent observations of data, called the posterior distribution. This method can help address issues with overfitting, since the model will always estimate model parameters with respect to the initial prior distribution to some extent.


##### Recommended installation

Install VS Code for code editor
Inside VS Code install Python, Jupyter extensions

Python - version 3.10.12

Install latest Bonsai version (v2.8.1 at this time)

Install dotnet-7.0 runtime 


##### Running in Python

The python portion is written in a Jupyter notebook and was built using Python kernel 3.10 (recommended)

Install packages from python directory:
  pip install -r requirements.txt

Make sure the appropriate python environment is selected for Jupyter.


##### Running in C#

Requires AvaloniaUI,


##### Running in Bonsai-Rx

Run setup script in Bonsai repository.

In Windows use `.\Setup.ps1`

In Linux use `./Setup.sh`