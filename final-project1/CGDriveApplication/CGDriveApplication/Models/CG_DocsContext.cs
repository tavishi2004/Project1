using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CGDriveApplication.Models
{
    public partial class CG_DocsContext : DbContext
    {
        public CG_DocsContext()
        {
        }

        public CG_DocsContext(DbContextOptions<CG_DocsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Documents> Documents { get; set; }
        public virtual DbSet<Folder> Folder { get; set; }
        public virtual DbSet<UserTable> UserTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=CG_Docs;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocId);

                entity.Property(e => e.DocId).HasColumnName("Doc_Id");

                entity.Property(e => e.ContentType)
                    .HasColumnName("content_Type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DCreatedAt)
                    .HasColumnName("D_CreatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.DCreatedBy).HasColumnName("D_CreatedBy");

                entity.Property(e => e.DocName)
                    .IsRequired()
                    .HasColumnName("Doc_Name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FolDocId).HasColumnName("Fol_doc_Id");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("Is_Deleted")
                    .HasDefaultValueSql("'0'");


                entity.Property(e => e.IsFavourite)
                    .HasColumnName("Is_Favourite")
                    .HasDefaultValueSql("'0'");
                entity.HasOne(d => d.DCreatedByNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DCreatedBy)
                    .HasConstraintName("FK__Documents__D_Cre__6383C8BA");

                entity.HasOne(d => d.FolDoc)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.FolDocId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Documents__Fol_d__6477ECF3");
            });

            modelBuilder.Entity<Folder>(entity =>
            {
                entity.Property(e => e.FolderId).HasColumnName("Folder_Id");

                entity.Property(e => e.FCreatedAt)
                    .HasColumnName("F_CreatedAt")
                    .HasColumnType("datetime");

                entity.Property(e => e.FCreatedBy).HasColumnName("F_CreatedBy");

                entity.Property(e => e.FolderName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("Is_Deleted")
                    .HasDefaultValueSql("('0')");

                entity.Property(e => e.IsFavourite)
                    .HasColumnName("Is_Favourite")
                    .HasDefaultValueSql("'0'");
                entity.HasOne(d => d.FCreatedByNavigation)
                    .WithMany(p => p.Folder)
                    .HasForeignKey(d => d.FCreatedBy)
                    .HasConstraintName("FK__Folder__Is_Delet__5FB337D6");
            });

            modelBuilder.Entity<UserTable>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("User_table");

                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("Created_At")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasColumnName("User_Password")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
