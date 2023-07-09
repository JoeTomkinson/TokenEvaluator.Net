# TokenEvaluator.Net

### Build Status
[![main](https://github.com/JoeTomkinson/TokenEvaluator.Net/actions/workflows/main-build.yml/badge.svg)](https://github.com/JoeTomkinson/TokenEvaluator.Net/actions/workflows/main-build.yml)
[![develop](https://github.com/JoeTomkinson/TokenEvaluator.Net/actions/workflows/develop-build.yml/badge.svg)](https://github.com/JoeTomkinson/TokenEvaluator.Net/actions/workflows/develop-build.yml)

These are currently failing due to an issue with GitHub action directories, working to resolve that at the moment.

## Description
TokenEvaluator.Net is a simple and useful library designed to measure and calculate the token count of given text inputs, as per the specifics of the language model specified by the user. This tool is crucial for efficient resource management when dealing with AI language models, such as OpenAI's GPT-3.5-turbo and others.

By providing a comprehensive and detailed evaluation of the token count, this library assists developers in understanding the cost, performance, and optimization aspects of their AI language model interactions.

Whether you're running an AI chatbot, a content generator, or any application that leverages AI language models, understanding your token usage is fundamental. TokenEvaluator.Net fills this gap, offering a clear, accurate, and easy-to-use solution.

## Features:

1. **Precise token count calculations** aligned with the specified language model
2. Support for a diverse **array of popular AI language models**
3. **Efficient and lightweight architecture** suitable for both integrated and standalone usage
4. **Open-source**, fostering community contributions and ongoing enhancement

Unlock the power of accurate token count understanding with TokenEvaluator.Net - the essential tool for AI developers.

## Supported Tokenizers
These are the currently supported tokenizers:

- CL100K
- P50K

# Getting Started

TokenEvaluator.Net can be used via dependency injection, or an instance can be created using a tightly-coupled factory class.

### Dependency Injection
If you want to be able to inject an instance of this client into multiple methods, then you can make use of the libraries dependency injection extension to add all of the required interfaces and implementations to your service collection.

```C#
using TokenEvaluator.Net.Dependency;

// Init a service collection, use the extension method to add the library services.
IServiceCollection services = new ServiceCollection();
services.AddTokenEvaluator.NetServices();
services.AddSingleton<ITokenEvaluatorClient, TokenEvaluatorClient>();
var serviceProvider = services.BuildServiceProvider();
```

Then simply inject the service into your class constructors like so:

```C#
internal const string GeneratedText = "The quick, brown fox—enamored by the moonlit night—jumped over 10 lazily sleeping dogs near 123 Elm St. at approximately 7:30 PM. Isn't text tokenization interesting?";

public ClassConstructor(ITokenEvaluatorClient tokenClient)
{
    // Set token encoding type
    tokenClient.SetDefaultTokenEncoding(EncodingType.Cl100kBase);
    var tokenCount = tokenClient.EncodedTokenCount(GeneratedText);

    // or choose a supported model
    tokenClient.SetDefaultTokenEncodingForModel(ModelType.TextDavinci003);
    var tokenCount = tokenClient.EncodedTokenCount(GeneratedText);
}
```

### Factory Implementation

Using this as a concrete, tightly-coupled implementation is fairly straightforward. Simply use the below code and all internal interface and service references will be initialised and tightly-coupled. This is difficult to write tests for within your application, but ultimately is the easiest way to implement the client.

```C#

using TokenEvaluator.Net;

var client = TokenEvaluatorClientFactory.Create();
client.SetDefaultTokenEncoding(EncodingType.Cl100kBase);
var tokenCount = client.EncodedTokenCount(Constants.GeneratedText);
```
