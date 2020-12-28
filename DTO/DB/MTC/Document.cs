using Microsoft.SqlServer.Types;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTO.DB.MTC
{
    [Table("DocumentRepository")]
    public class Document
    {
        [Key]
        [Column("stream_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid StreamId { get; set; }

        [Column("file_stream")]
        public byte[] FileStream { get; set; }

        [Column("name")]
        [StringLength(255)]
        public string Name { get; set; }

        [Column("path_locator")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public SqlHierarchyId PathLocator { get; set; }

        [Column("parent_path_locator")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public SqlHierarchyId ParentPathLocator { get; set; }

        [Column("file_type")]
        [StringLength(255)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string FileType { get; set; }

        [Column("cached_file_size")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long? CachedFileSize { get; set; }

        [Column("creation_time")]
        public DateTimeOffset CreationTime { get; set; }

        [Column("last_write_time")]
        public DateTimeOffset LastWriteTime { get; set; }

        [Column("last_access_time")]
        public DateTimeOffset? LastAccessTime { get; set; }

        [Column("is_directory")]
        public bool IsDirectory { get; set; }

        [Column("is_offline")]
        public bool IsOffline { get; set; }

        [Column("is_hidden")]
        public bool IsHidden { get; set; }

        [Column("is_readonly")]
        public bool IsReadOnly { get; set; }

        [Column("is_archive")]
        public bool IsArchive { get; set; }

        [Column("is_system")]
        public bool IsSystem { get; set; }

        [Column("is_temporary")]
        public bool IsTemporary { get; set; }
    }
}
