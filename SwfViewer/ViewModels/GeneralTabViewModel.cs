using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SwfSharp;
using SwfSharp.Tags;

namespace SwfViewer.ViewModels
{
    class GeneralTabViewModel : SecondaryViewModel
    {
        private FileAttributesTag _fileAttributes;
        private int _frameCount;

        public GeneralTabViewModel(MainViewModel mainViewModel) : base (mainViewModel)
        {
        }

        public Visibility Visibility
        {
            get { return _swf != null ? Visibility.Visible : Visibility.Hidden; }
        }

        public byte Version
        {
            get { return _swf == null ? (byte)0 : _swf.Header.Version; }
            set { DoIfNotNull(_swf, () => _swf.Header.Version = value); }
        }

        public SwfFileCompression Compression
        {
            get { return _swf == null ? SwfFileCompression.None : _swf.Header.Compression; }
            set { DoIfNotNull(_swf, () => _swf.Header.Compression = value); }
        }

        public bool UseDirectBlit
        {
            get { return _fileAttributes != null && _fileAttributes.UseDirectBlit; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.UseDirectBlit = value); }
        }

        public bool HasMetadata
        {
            get { return _fileAttributes != null && _fileAttributes.HasMetadata; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.HasMetadata = value); }
        }

        public bool SuppressCrossDomainCaching
        {
            get { return _fileAttributes != null && _fileAttributes.SuppressCrossDomainCaching; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.SuppressCrossDomainCaching = value); }
        }

        public bool ActionScript3
        {
            get { return _fileAttributes != null && _fileAttributes.ActionScript3; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.ActionScript3 = value); }
        }

        public bool SwfRelativeUrls
        {
            get { return _fileAttributes != null && _fileAttributes.SwfRelativeUrls; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.SwfRelativeUrls = value); }
        }

        public bool UseGPU
        {
            get { return _fileAttributes != null && _fileAttributes.UseGPU; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.UseGPU = value); }
        }

        public bool UseNetwork
        {
            get { return _fileAttributes != null && _fileAttributes.UseNetwork; }
            set { DoIfNotNull(_fileAttributes, () => _fileAttributes.UseNetwork = value); }
        }

        public int FrameCount
        {
            get { return _frameCount; }
        }

        public uint Size
        {
            get { return _swf == null ? 0 : _swf.Header.FileSize; }
        }

        public int Width
        {
            get { return _swf == null ? 0 : _swf.Header.Rect.Xmax / 20; }
            set { DoIfNotNull(_swf, () => _swf.Header.Rect.Xmax = value*20); }
        }

        public int Height
        {
            get { return _swf == null ? 0 : _swf.Header.Rect.Ymax / 20; }
            set { DoIfNotNull(_swf, () => _swf.Header.Rect.Ymax = value*20); }
        }

        public float FrameRate
        {
            get { return _swf == null ? 0 : _swf.Header.FrameRate; }
            set { DoIfNotNull(_swf, () => _swf.Header.FrameRate = value); }
        }

        public int TotalExports
        {
            get
            {
                return _swf == null
                    ? 0
                    : _swf.Tags.Where(t => t.TagType == TagType.ExportAssets)
                        .Sum(t => ((ExportAssetsTag) t).Records.Count);
            }
        }

        public int TotalTags
        {
            get { return _swf == null ? 0 : _swf.Tags.Count; }
        }

        public IList<string> Exports
        {
            get
            {
                return _swf == null
                    ? new List<string>(0)
                    : _swf.Tags.Where(t => t.TagType == TagType.ExportAssets)
                        .SelectMany(t => ((ExportAssetsTag) t).Records.Select(r => r.Name))
                        .ToList();
            }
        }

        public IList<string> Tags
        {
            get
            {
                return _swf == null
                    ? new List<string>(0)
                    : _swf.Tags.GroupBy(t => t.TagType).Select(group => string.Format("{0} ({1}) \u2013 total {2}", group.Key, (int)group.Key, group.Count())).ToList();
            }
        }

        protected override void Refresh()
        {
            _fileAttributes = _swf.Tags.FirstOrDefault(tag => tag.TagType == TagType.FileAttributes) as FileAttributesTag;
            _frameCount = _swf.Tags.Count(tag => tag.TagType == TagType.ShowFrame);
        }
    }
}
