using System;
using System.Collections.Generic;
using System.Linq;
using Root.Helpers;
using Root.MusicBeeApi;
using Root.MusicBeeApi.Abstract;

namespace Module.InboxAdder.Services
{
    public class InboxAddService : IInboxAddService
    {
        private readonly IMusicBeeApi _mbApi;
        
        public InboxAddService(IMusicBeeApi mbApi)
        {
            _mbApi = mbApi;
        }
        
        public void AddToLibrary(string filePath)
        {
            if (IsInLibrary(filePath))
            {
                return;
            }

            var currentIndex = GetLastIndex() + 1;
            
            MBApiHelper.CalcIndices(currentIndex, out var i1, out var i2);
            
            _mbApi.Library_AddFileToLibrary(filePath, LibraryCategory.Music);
            _mbApi.SetIndex(filePath, currentIndex, false);
            _mbApi.SetIndex1(filePath, i1, false);
            _mbApi.SetIndex2(filePath, i2, false);
            
            _mbApi.Library_CommitTagsToFile(filePath);
        }

        public void RetrieveToInbox(string filePath)
        {
            if (!IsInLibrary(filePath))
            {
                return;
            }
            
            _mbApi.Library_AddFileToLibrary(filePath, LibraryCategory.Inbox);
            
            _mbApi.ClearIndex(filePath, false);
            _mbApi.ClearIndex1(filePath, false);
            _mbApi.ClearIndex2(filePath, false);

            _mbApi.Library_CommitTagsToFile(filePath);
        }

        private bool IsInLibrary(string filePath)
        {
            return _mbApi.TryGetIndex(filePath, out var index) 
                   && index >= 0;
        }
        
        private int GetLastIndex()
        {
            return GetFilePaths()
                .Select(p => _mbApi.TryGetIndex(p, out var index)
                    ? index
                    : -1)
                .Max();
        }

        private IReadOnlyCollection<string> GetFilePaths()
        {
            if (!_mbApi.Library_QueryFilesEx("", out var filePaths))
            {
                throw new Exception("Error on receive files from library");
            }

            return filePaths;
        }
    }
}