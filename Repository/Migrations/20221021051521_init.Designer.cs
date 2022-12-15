﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(BookRepository))]
    [Migration("20221021051521_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_book_id");

                    b.Property<Guid>("AddressTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_type_id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("city");

                    b.Property<Guid>("CountryTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("country_type_id");

                    b.Property<string>("Line1")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("line_1");

                    b.Property<string>("Line2")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("line_2");

                    b.Property<string>("StateName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("state_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("zip_code");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Entities.AddressBook", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("last_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AddressBooks");
                });

            modelBuilder.Entity("Entities.Asset", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_book_id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.Property<string>("DownloadUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("download_url");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("file_name");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("file_type");

                    b.Property<decimal>("Size")
                        .HasColumnType("numeric")
                        .HasColumnName("size");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("UserId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("Entities.Email", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_book_id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email_address");

                    b.Property<Guid>("EmailTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("email_type_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("UserId");

                    b.ToTable("email");
                });

            modelBuilder.Entity("Entities.Phone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("AddressBookId")
                        .HasColumnType("uuid")
                        .HasColumnName("address_book_id");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)")
                        .HasColumnName("phone_number");

                    b.Property<Guid>("PhoneTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("phone_type_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("AddressBookId");

                    b.HasIndex("UserId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("Entities.RefSet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<string>("Set")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("set");

                    b.HasKey("Id");

                    b.ToTable("RefSets");
                });

            modelBuilder.Entity("Entities.RefSetTerm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("RefSetId")
                        .HasColumnType("uuid")
                        .HasColumnName("ref_set_id");

                    b.Property<Guid>("RefTermId")
                        .HasColumnType("uuid")
                        .HasColumnName("ref_term_id");

                    b.HasKey("Id");

                    b.HasIndex("RefSetId");

                    b.HasIndex("RefTermId");

                    b.ToTable("RefSetTerm");
                });

            modelBuilder.Entity("Entities.RefTerm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("description");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("key");

                    b.HasKey("Id");

                    b.ToTable("RefTerms");
                });

            modelBuilder.Entity("Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("user_name");

                    b.HasKey("Id");

                    b.HasAlternateKey("UserName");

                    b.ToTable("user");
                });

            modelBuilder.Entity("Entities.Address", b =>
                {
                    b.HasOne("Entities.AddressBook", "AddressBook")
                        .WithMany()
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.AddressBook", b =>
                {
                    b.HasOne("Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Asset", b =>
                {
                    b.HasOne("Entities.AddressBook", "AddressBook")
                        .WithMany()
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Email", b =>
                {
                    b.HasOne("Entities.AddressBook", "AddressBook")
                        .WithMany()
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.Phone", b =>
                {
                    b.HasOne("Entities.AddressBook", "AddressBook")
                        .WithMany()
                        .HasForeignKey("AddressBookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AddressBook");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Entities.RefSetTerm", b =>
                {
                    b.HasOne("Entities.RefSet", "RefSet")
                        .WithMany()
                        .HasForeignKey("RefSetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.RefTerm", "RefTerm")
                        .WithMany()
                        .HasForeignKey("RefTermId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RefSet");

                    b.Navigation("RefTerm");
                });
#pragma warning restore 612, 618
        }
    }
}
