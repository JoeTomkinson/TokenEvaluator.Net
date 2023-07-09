﻿using TextTokenizationEvaluator.Core.Constants;
using TextTokenizationEvaluator.Core.Models;
using TextTokenizationEvaluator.Core.Services.Contracts;
using TextTokenizationEvaluator.Core.Tokenizers;

namespace TextTokenizationEvaluator.Core.Services;

public class EncodingService : IEncodingService
{
    private readonly IModelEncodingProvider _modelEncodingProvider;
    private readonly BaseTokenizerProvider _tokenizerProvider;
    private Cl100kBase _cl100k_base;
    private P50kBase _p50KBase;

    public EncodingService(IModelEncodingProvider modelEncodingProvider, BaseTokenizerProvider tokenizerProvider)
    {
        _modelEncodingProvider = modelEncodingProvider;
        _tokenizerProvider = tokenizerProvider;

        // get directory for cached data
        var _localAppDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var directory = Path.Combine(_localAppDataDirectory, FileConstants.PAIRED_BYTE_ENCODING_URLCACHE_DIRECTORY);

        // set default directory for paired byte encoding files
        _tokenizerProvider.SetPairedByteEncodingDirectory(directory);

        // initialise our available tiktoken base classes
        _cl100k_base = new Cl100kBase(tokenizerProvider);
        _p50KBase = new P50kBase(tokenizerProvider);
    }

    public void SetPairedByteEncodingDirectory(string directory)
    {
        // set default directory for paired byte encoding files
        _tokenizerProvider.SetPairedByteEncodingDirectory(directory);

        // re-initialise our available tiktoken base classes
        _cl100k_base = new Cl100kBase(_tokenizerProvider);
        _p50KBase = new P50kBase(_tokenizerProvider);
    }

    public TextTokenEncoding GetEncodingFromModel(ModelType modelType)
    {
       return GetEncoding(_modelEncodingProvider.GetEncodingForModel(modelType));
    }

    public Task<TextTokenEncoding> GetEncodingFromModelAsync(ModelType modelType)
    {
        return GetEncodingAsync(_modelEncodingProvider.GetEncodingForModel(modelType));
    }

    public TextTokenEncoding GetEncoding(EncodingType encodingType)
    {
        switch (encodingType)
        {
            case EncodingType.P50kBase:
                {
                    return _p50KBase.GetEncodingAsync().Result;
                }
            case EncodingType.Cl100kBase:
                {
                    return _cl100k_base.GetEncodingAsync().Result;
                }
            default:
                throw new NotImplementedException();
        }
    }

    public async Task<TextTokenEncoding> GetEncodingAsync(EncodingType encodingType)
    {
        switch (encodingType)
        {
            case EncodingType.P50kBase:
                {
                    return await _p50KBase.GetEncodingAsync();
                }
            case EncodingType.Cl100kBase:
                {
                    return await _cl100k_base.GetEncodingAsync();
                }
            default:
                throw new NotImplementedException();
        }
    }
}