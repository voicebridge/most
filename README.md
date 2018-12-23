# Most (Preview)

## Installation
Nuget package will be published in a few days.

## What is Most?

Most is a framework which enables you to write an assistant on a .net based webhook or serveless function for Alexa and Google Assistant. 
Both Alexa and Google Assistant offer 3rd party integrations via webhooks. Most implements the JSON object models for both Alexa and Google Assistant. It provides a layer of abstraction to ensure that your assistant can run on all platforms without extra coding effort.

## Why Most?

Most is the polish word for bridge.

## How does it work?

Most has a 3 stage process for handling conversation requests:
1) Model Building: Alexa Skill request or Google Action request is transformed into a platform agnostic model. This is done automatically for you but if it is not sufficient, you can inject your own input model builder
2) Processing: When you setup rules, you define what to do when rules are invoked. These are called Request Handlers. The output of request handlers are objects referred to as Virtual Directives. These are simply object models that describe what to send the user. This stage is completley platform agnostic.
3) Response Building: Virtual directives are transformed into platform specific object models. There are a few virtual directives and processors implemented already. The goal is to implement all of them in the coming weeks. You can object create your own and inject them into the framework. 


## Quick Start Guide

This assumes that you have defined your Alexa language model or your Google Actions language model. Check the respective platforms on how to do this. This is where you define the intents and sample phrases.

### Step 1: Create your assistant

The simplest way to create an assistant is through the Assistant object. This has a simple fluent interface that gives you direct access to all the framework features
```csharp
private Assistant GetAssistant()
{
    var assistant = new Assistant();
    assistant
        .OnIntents("ScoreIntent", "HockeyIntent")
        .When(x => x.RequestModel.GetParameterValue(ParamNames.Team) == "sj")
        .Say("Sharks beat the wild! Today they will play the jets");

    assistant
        .OnIntents("ScoreIntent", "HockeyIntent", "actions.intent.Text")
        .When(x => x.RequestModel.GetParameterValue(ParamNames.Team) == "vn")
        .Say("Not sure about vancouver");

    assistant
        .OnIntent("LatestHockeyScoresIntent")
        .Do(CreatePlayMediaDirective);

    assistant
        .OnAnyIntent()
        .When(x => !x.RequestModel.ParameterHasValue(ParamNames.Team))
        .AskFor(ParamNames.Team, "What team would you like?");

    return assistant;
}
```

### Step 2: Setup

Depending on which platform you are targeting, you will use different Request and Response types. 
(These examples are for AWS Lambda Functions)

#### Alexa Skill

```csharp
public async Task<SkillResponse> FunctionHandler(SkillRequest input, ILambdaContext context)
{
    var engine = GetAssistant()
        .AlexaEngineBuilder()
        .SetLogger(new Logger())
        .Build();
    
    return await Process(engine, input, context);
}
```
#### Google Assistant Action
```csharp
public async Task<AppResponse> ActionHandler(AppRequest input, ILambdaContext context)
{
    var engine = GetAssistant()
        .GoogleEngineBuilder()
        .SetLogger(new Logger())
        .Build();
    
    return await Process(engine, input, context);
}
```