// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Music_Portal.Domain.Core;

#nullable disable

namespace Music_Portal.Domain.Core.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20211115131710_AddArtistAndTrackWiki")]
    partial class AddArtistAndTrackWiki
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Music_Portal.Domain.Core.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Biography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Listeners")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("Playcount")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("Music_Portal.Domain.Core.SimilarArtistRelations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SimilarArtistId")
                        .HasColumnType("int");

                    b.Property<int>("SimilarToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SimilarArtistsToArtist");
                });

            modelBuilder.Entity("Music_Portal.Domain.Core.Track", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int");

                    b.Property<int>("Listeners")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Playcount")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Wiki")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Tracks");
                });

            modelBuilder.Entity("Music_Portal.Domain.Core.Track", b =>
                {
                    b.HasOne("Music_Portal.Domain.Core.Artist", "Artist")
                        .WithMany("Tracks")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Music_Portal.Domain.Core.Artist", b =>
                {
                    b.Navigation("Tracks");
                });
#pragma warning restore 612, 618
        }
    }
}
