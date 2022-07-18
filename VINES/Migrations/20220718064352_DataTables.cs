using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VINES.Migrations
{
    public partial class DataTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "advertisementType",
                columns: table => new
                {
                    advertisementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    institutionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisementType", x => x.advertisementID);
                });

            migrationBuilder.CreateTable(
                name: "auditCategories",
                columns: table => new
                {
                    auditCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    auditCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_auditCategories", x => x.auditCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "communityPostCategories",
                columns: table => new
                {
                    communityPostCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    communityPostCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_communityPostCategories", x => x.communityPostCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "diseases",
                columns: table => new
                {
                    diseaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diseases", x => x.diseaseID);
                });

            migrationBuilder.CreateTable(
                name: "forumCategory",
                columns: table => new
                {
                    forumCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    forumCategory = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forumCategory", x => x.forumCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    genderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.genderID);
                });

            migrationBuilder.CreateTable(
                name: "institutionType",
                columns: table => new
                {
                    institutionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    institutionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institutionType", x => x.institutionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "IPAddresses",
                columns: table => new
                {
                    IPAddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    violations = table.Column<int>(type: "int", nullable: false),
                    isBlocked = table.Column<bool>(type: "bit", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPAddresses", x => x.IPAddressID);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    postID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.postID);
                });

            migrationBuilder.CreateTable(
                name: "recommendedInstitutions",
                columns: table => new
                {
                    recommendedInstitutionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    institutionID = table.Column<int>(type: "int", nullable: false),
                    userPreferenceID = table.Column<int>(type: "int", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_recommendedInstitutions", x => x.recommendedInstitutionID);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    roleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleID = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    middleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genderID = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    emailAuth = table.Column<bool>(type: "bit", nullable: false),
                    isBlocked = table.Column<bool>(type: "bit", nullable: false),
                    isLocked = table.Column<bool>(type: "bit", nullable: false),
                    dateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    failedAttempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "vaccinePreference",
                columns: table => new
                {
                    vaccinePreferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientPreferenceID = table.Column<int>(type: "int", nullable: false),
                    vaccineID = table.Column<int>(type: "int", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccinePreference", x => x.vaccinePreferenceID);
                });

            migrationBuilder.CreateTable(
                name: "webPages",
                columns: table => new
                {
                    webPageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sourcesID = table.Column<int>(type: "int", nullable: false),
                    webURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    pageTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pageContents = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_webPages", x => x.webPageID);
                });

            migrationBuilder.CreateTable(
                name: "vaccines",
                columns: table => new
                {
                    vaccineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseaseID = table.Column<int>(type: "int", nullable: false),
                    vaccineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaccines", x => x.vaccineID);
                    table.ForeignKey(
                        name: "FK_vaccines_diseases_diseaseID",
                        column: x => x.diseaseID,
                        principalTable: "diseases",
                        principalColumn: "diseaseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "institutions",
                columns: table => new
                {
                    institutionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    institutionTypeID = table.Column<int>(type: "int", nullable: false),
                    institutionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    lat = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institutions", x => x.institutionID);
                    table.ForeignKey(
                        name: "FK_institutions_institutionType_institutionTypeID",
                        column: x => x.institutionTypeID,
                        principalTable: "institutionType",
                        principalColumn: "institutionTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityPost",
                columns: table => new
                {
                    communityPostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    communityPostTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    communityPostCategoryID = table.Column<int>(type: "int", nullable: false),
                    communityPostContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityPost", x => x.communityPostID);
                    table.ForeignKey(
                        name: "FK_CommunityPost_communityPostCategories_communityPostCategoryID",
                        column: x => x.communityPostCategoryID,
                        principalTable: "communityPostCategories",
                        principalColumn: "communityPostCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommunityPost_posts_postID",
                        column: x => x.postID,
                        principalTable: "posts",
                        principalColumn: "postID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "forumPost",
                columns: table => new
                {
                    forumPostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    forumCategoryID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    forumContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forumPost", x => x.forumPostID);
                    table.ForeignKey(
                        name: "FK_forumPost_forumCategory_forumCategoryID",
                        column: x => x.forumCategoryID,
                        principalTable: "forumCategory",
                        principalColumn: "forumCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forumPost_posts_postID",
                        column: x => x.postID,
                        principalTable: "posts",
                        principalColumn: "postID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forumPost_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    patientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    showAds = table.Column<bool>(type: "bit", nullable: false),
                    isSubscribed = table.Column<bool>(type: "bit", nullable: false),
                    subStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    subEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.patientID);
                    table.ForeignKey(
                        name: "FK_patients_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "advertisers",
                columns: table => new
                {
                    advertiserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    institutionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisers", x => x.advertiserID);
                    table.ForeignKey(
                        name: "FK_advertisers_institutions_institutionID",
                        column: x => x.institutionID,
                        principalTable: "institutions",
                        principalColumn: "institutionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_advertisers_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "institutionsVaccines",
                columns: table => new
                {
                    institutionVaccineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseaseID = table.Column<int>(type: "int", nullable: false),
                    institutionID = table.Column<int>(type: "int", nullable: false),
                    vaccineID = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_institutionsVaccines", x => x.institutionVaccineID);
                    table.ForeignKey(
                        name: "FK_institutionsVaccines_diseases_diseaseID",
                        column: x => x.diseaseID,
                        principalTable: "diseases",
                        principalColumn: "diseaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_institutionsVaccines_institutions_vaccineID",
                        column: x => x.vaccineID,
                        principalTable: "institutions",
                        principalColumn: "institutionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_institutionsVaccines_vaccines_institutionID",
                        column: x => x.institutionID,
                        principalTable: "vaccines",
                        principalColumn: "vaccineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "forumComment",
                columns: table => new
                {
                    forumCommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    forumPostID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_forumComment", x => x.forumCommentID);
                    table.ForeignKey(
                        name: "FK_forumComment_forumPost_forumPostID",
                        column: x => x.forumPostID,
                        principalTable: "forumPost",
                        principalColumn: "forumPostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_forumComment_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookmarks",
                columns: table => new
                {
                    bookmarkID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    postID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookmarks", x => x.bookmarkID);
                    table.ForeignKey(
                        name: "FK_bookmarks_patients_patientID",
                        column: x => x.patientID,
                        principalTable: "patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookmarks_posts_postID",
                        column: x => x.postID,
                        principalTable: "posts",
                        principalColumn: "postID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientPreferences",
                columns: table => new
                {
                    patientPreferenceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    diseaseID = table.Column<int>(type: "int", nullable: false),
                    patientID = table.Column<int>(type: "int", nullable: false),
                    budget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientPreferences", x => x.patientPreferenceID);
                    table.ForeignKey(
                        name: "FK_patientPreferences_diseases_diseaseID",
                        column: x => x.diseaseID,
                        principalTable: "diseases",
                        principalColumn: "diseaseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patientPreferences_patients_patientID",
                        column: x => x.patientID,
                        principalTable: "patients",
                        principalColumn: "patientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "advertisement",
                columns: table => new
                {
                    advertisementID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    advertiserID = table.Column<int>(type: "int", nullable: false),
                    imgsrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    textContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    advertiseTypeID = table.Column<int>(type: "int", nullable: false),
                    advertisementTitle = table.Column<int>(type: "int", nullable: false),
                    clicks = table.Column<int>(type: "int", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    postID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_advertisement", x => x.advertisementID);
                    table.ForeignKey(
                        name: "FK_advertisement_advertisers_advertiserID",
                        column: x => x.advertiserID,
                        principalTable: "advertisers",
                        principalColumn: "advertiserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_advertisement_posts_postID",
                        column: x => x.postID,
                        principalTable: "posts",
                        principalColumn: "postID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_advertisement_advertiserID",
                table: "advertisement",
                column: "advertiserID");

            migrationBuilder.CreateIndex(
                name: "IX_advertisement_postID",
                table: "advertisement",
                column: "postID");

            migrationBuilder.CreateIndex(
                name: "IX_advertisers_institutionID",
                table: "advertisers",
                column: "institutionID");

            migrationBuilder.CreateIndex(
                name: "IX_advertisers_userID",
                table: "advertisers",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_bookmarks_patientID",
                table: "bookmarks",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_bookmarks_postID",
                table: "bookmarks",
                column: "postID");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPost_communityPostCategoryID",
                table: "CommunityPost",
                column: "communityPostCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityPost_postID",
                table: "CommunityPost",
                column: "postID");

            migrationBuilder.CreateIndex(
                name: "IX_forumComment_forumPostID",
                table: "forumComment",
                column: "forumPostID");

            migrationBuilder.CreateIndex(
                name: "IX_forumComment_userID",
                table: "forumComment",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_forumPost_forumCategoryID",
                table: "forumPost",
                column: "forumCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_forumPost_postID",
                table: "forumPost",
                column: "postID");

            migrationBuilder.CreateIndex(
                name: "IX_forumPost_userID",
                table: "forumPost",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_institutions_institutionTypeID",
                table: "institutions",
                column: "institutionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_institutionsVaccines_diseaseID",
                table: "institutionsVaccines",
                column: "diseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_institutionsVaccines_institutionID",
                table: "institutionsVaccines",
                column: "institutionID");

            migrationBuilder.CreateIndex(
                name: "IX_institutionsVaccines_vaccineID",
                table: "institutionsVaccines",
                column: "vaccineID");

            migrationBuilder.CreateIndex(
                name: "IX_patientPreferences_diseaseID",
                table: "patientPreferences",
                column: "diseaseID");

            migrationBuilder.CreateIndex(
                name: "IX_patientPreferences_patientID",
                table: "patientPreferences",
                column: "patientID");

            migrationBuilder.CreateIndex(
                name: "IX_patients_userID",
                table: "patients",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_vaccines_diseaseID",
                table: "vaccines",
                column: "diseaseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advertisement");

            migrationBuilder.DropTable(
                name: "advertisementType");

            migrationBuilder.DropTable(
                name: "auditCategories");

            migrationBuilder.DropTable(
                name: "bookmarks");

            migrationBuilder.DropTable(
                name: "CommunityPost");

            migrationBuilder.DropTable(
                name: "forumComment");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "institutionsVaccines");

            migrationBuilder.DropTable(
                name: "IPAddresses");

            migrationBuilder.DropTable(
                name: "patientPreferences");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "recommendedInstitutions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "vaccinePreference");

            migrationBuilder.DropTable(
                name: "webPages");

            migrationBuilder.DropTable(
                name: "advertisers");

            migrationBuilder.DropTable(
                name: "communityPostCategories");

            migrationBuilder.DropTable(
                name: "forumPost");

            migrationBuilder.DropTable(
                name: "vaccines");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "institutions");

            migrationBuilder.DropTable(
                name: "forumCategory");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "diseases");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "institutionType");
        }
    }
}
