# Bayesian Linear Regression


## Exercise

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

- in Python
- in C#
- in Bonsai

For personal reference, the figure is on page 155.


## Problem Overview

The exercise is about implementing a Bayesian model within the framework of linear regression. The goal is to recreate the figure shown in the reference to create an online learning model where the posterior distribution of the Bayesian model will update continuously with new observations.


## Problem Description

Maximizing likelihood inherently drives the addition of more parameters to the model to boost accuracy. This leads to increased model complexity, which may compromise the models ability to generalize to new data. This is the problem of overfitting.

Regularization techniques can help to offset the problem of overfitting by penalizing superfluous parameters, but their efficacy depends on the specific regularization employed, since certain basis functions can skew the results towards bias or variance.

In contrast, the Bayesian approach makes use of a prior distribution of expected model parameters. The expected probabilities then update through subsequent observations of data, called the posterior distribution. This method can help address issues with overfitting, since the model will always estimate model parameters with respect to the initial prior distribution to some extent. However, Bayesian regression can be computationally intensive and hinges on the choice of priors.

In online learning, the bayesian model will update the posterior distribution with every new data point it observes.

In both the Python and Bonsai/C# repositories, there is code to generate the figure in its original form, as well as an "online learning" component to demsotrate how the bayesian regression model changes with each new data sample observed.


## Recommended installation

Install VS Code for code editor
Install Python version 3.10.12
Inside VS Code install Python extension, Jupyter extension


## Running in Python

Jupyter notebook is required. Python version 3.10.12 is recommended to avoid package conflicts.

It is recommended to use a virtual environment for reproducability. For example, navigate to the repo directory and run:

`python -m venv .venv`

Then, activate this environment by running:

`.venv\scripts\activate` in Windows command line, or

`source .venv/bin/activate` in Linux terminal.

Install the required packages from the python directory:

`pip install -r python/requirements.txt`

Make sure the appropriate python environment is selected for Jupyter. Look for the option in the top right corner of VS Code to change Jupyter kernel to the newly setup virtual environment.

Hit the `Run All` option.

If a pop up option appears in VS Code which says Ipykernel is required, install it.

The first section creates classes for plotting, data generation, and Bayesian linear regression.

The next section creates the original 3x4 panel figure.

The last section creates an online learning class that updates the likelihood, posterior distribution, and random data samples every second with each observation of a new data sample. The posterior is updated sequentially, such that the most recently calculated posterior is fed into the model as the prior, which is then updated based on the likelihood of the most recently observed datapoint.

The `Interrupt` button can be pressed at any time to halt the loop.


## Running in Bonsai-Rx

Bonsai version 2.8.1 is required. You can automatically setup a Bonsai environment by running the setup script.

In Windows, open PowerShell. Navigate to the `Bonsai\bonsai_env` directory and run `.\Setup.ps1`.

This should automatically download and install the Bonsai executable and required packages. 

Alternatively, you can install Bonsai globally on the machine or in a seperate folder and manually install the packages found in the Bonsai.config file by going to `Manage Packages` and installing them.

Once Bonsai and the required packages are installed, launch Bonsai and select `Open file` from the menu. Navigate to the subdirectory called `workflows` and select one of the workflows.

For workflows that calculate the posterior by tracing all data points back to the prior:
- `BayesianLinearRegression`: when this workflow is run, it will render the original 3x4 panel figure at runtime. Calculates the posterior using all observed data points traced back to the prior.
- `OnlineBayesianLinearRegression_KeyDown`: whenever a key is pressed, a new data sample is generated and the likelihood/posterior distribution is updated. Calculates the posterior using all observed data points traced back to the prior.
- `OnlineBayesianLinearRegression_Timer`: a new data sample is generated and the likelihood/posterior distribution is updated every second. Calculates the posterior using all observed data points traced back to the prior.

For workflows that take only the likelihood of the last data point to update the posterior in a sequential manner:
- `OnlineBayesianLinearRegression_KeyDown_SequentialUpdates`: whenever a key is pressed, a new data sample is generated and the likelihood/posterior distribution is updated. Calculates the posterior by combining the prior with the likelihood of the last data point scaled by a factor determined by the precision (beta).
- `OnlineBayesianLinearRegression_Timer_SequentialUpdates`: a new data sample is generated and the likelihood/posterior distribution is updated every second. Calculates the posterior by combining the prior with the likelihood of the last data point scaled by a factor determined by the precision (beta).

If the visualizer does not display, double click the `FigureGeneration` node (in the main workflow) while the workflow is running to bring up the visualizer window. Additionally, you can right click the `FigureGeneration` node, go to `Show Visualizer`, and then select the `TableLayoutPanelVisualizer`. You may have to restart the workflow to see the figure at runtime.