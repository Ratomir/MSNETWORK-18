using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Spatial;
using Microsoft.Extensions.DependencyInjection;
using MSNetwork18.CLI.DI_Container;
using MSNetwork18.DAL.Interface;
using MSNetwork18.Model.Email;
using MSNetwork18.Model.WorldBank;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MSNetwork18.CLI.Programs
{
    public class DocumentProgram : BaseProgram
    {
        enum ReadOptionEnum
        {
            Dynamic = 1,
            Document = 2,
            Model = 3,
            ToJSONOutput = 4
        };

        enum InsertOptionEnum
        {
            FromFile = 1,
            FromAnonymous = 2,
            FromModel = 3
        }

        public ISQLDocumentRepository _repository { get; set; }

        public DocumentProgram()
        {
            _repository = DIProvider.GetServiceProvider().GetService<ISQLDocumentRepository>();
        }

        public async Task ReadDocument()
        {
            string databaseName = "";
            DocumentCollection collectionName = null;

            if (!InsertCollAndDatabase(ref databaseName, ref collectionName))
            {
                Warning("Collection >>> " + collectionName + " <<< don't exist.");
                return;
            }

            WriteLine("1. Dynamic");
            WriteLine("2. Document");
            WriteLine("3. Model");
            WriteLine("4. To JSON output");

            ReadOptionEnum option = (ReadOptionEnum)ProgramHelper.EnterInt("");
            string id = ProgramHelper.EnterText("Enter document id");

            switch (option)
            {
                case ReadOptionEnum.Dynamic:
                    {
                        dynamic documentDynamic = await _repository.ReadDocumentByIdAsync<dynamic>(id, UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id));

                        WriteLine("Id:\t" + documentDynamic.id);
                        WriteLine("Country code:\t" + documentDynamic.countrycode);
                        WriteLine("Country name:\t" + documentDynamic.countryname);

                        List<dynamic> lstMajorSector = documentDynamic.majorsector_percent.ToObject<List<dynamic>>();

                        foreach (var majorSector in lstMajorSector)
                        {
                            WriteLine("  Sector name:\t\t" + majorSector.Name);
                            WriteLine("  Sector percent:\t" + majorSector.Percent);
                            ProgramHelper.Divider();
                        }

                        break;
                    }
                case ReadOptionEnum.Document:
                    {
                        Document document = await _repository.ReadDocumentByIdAsync<Document>(id, UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id));

                        WriteLine("Id:\t" + document.GetPropertyValue<string>("id"));
                        WriteLine("Country code:\t" + document.GetPropertyValue<string>("countrycode"));
                        WriteLine("Country name:\t" + document.GetPropertyValue<string>("countryname"));
                        JArray childArrayDocument = document.GetPropertyValue<JArray>("majorsector_percent");

                        foreach (var item in childArrayDocument)
                        {
                            WriteLine("  Sector name:\t\t" + item["Name"]);
                            WriteLine("  Sector percent:\t" + item["Percent"]);
                            ProgramHelper.Divider();
                        }
                        break;
                    }
                case ReadOptionEnum.Model:
                    {
                        WorldBankModel model = await _repository.ReadDocumentByIdAsync<WorldBankModel>(id, UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id));

                        WriteLine("Id:\t" + model.Id);
                        WriteLine("Country code:\t" + model.Countrycode);
                        WriteLine("Country name:\t" + model.Countryname);

                        ProgramHelper.Divider();
                        foreach (var majorSector in model.MajorsectorPercent)
                        {
                            WriteLine("  Sector name:\t\t" + majorSector.Name);
                            WriteLine("  Sector percent:\t" + majorSector.Percent);
                            ProgramHelper.Divider();
                        }

                        break;
                    }
                case ReadOptionEnum.ToJSONOutput:
                    {
                        Document document = await _repository.ReadDocumentByIdAsync<Document>(id, UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id));

                        WriteLine(document.ToString());
                        break;
                    }
                default:
                    break;
            }

        }

        public async Task DeleteDocument()
        {
            string databaseName = ProgramHelper.ReadDatabaseName();
            string collectionName = ProgramHelper.ReadCollectionName();
            string id = ProgramHelper.EnterText("Enter document id");

            bool result = await _repository.DeleteDocument(databaseName, collectionName, id);

            if (result)
            {
                Success("Document deleted.");
            }
            else
            {
                Error("Document was not deleted.");
            }
        }

        public async Task InsertDocument()
        {
            string databaseName = "";
            DocumentCollection collectionName = null;

            if (!InsertCollAndDatabase(ref databaseName, ref collectionName))
            {
                Warning("Collection >>> " + collectionName + " <<< don't exist.");
                collectionName = await _collectionRepository.CreateCollection(databaseName, collectionName.Id);
                ProgramHelper.Wait();
            }

            WriteLine("1. New document from file");
            WriteLine("2. New document from anonymous type");
            WriteLine("3. New document from model");

            InsertOptionEnum option = (InsertOptionEnum)ProgramHelper.EnterInt("");

            switch (option)
            {
                case InsertOptionEnum.FromFile:
                    {
                        using (StreamReader file = new StreamReader(@"Scripts\zips.json"))
                        {
                            string line;
                            int count = 0;
                            while ((line = file.ReadLine()) != null && count != 100)
                            {
                                byte[] byteArray = Encoding.UTF8.GetBytes(line);
                                using (MemoryStream stream = new MemoryStream(byteArray))
                                {
                                    Document newDocument = await _repository.InsertDocument(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id), JsonSerializable.LoadFrom<Document>(stream));

                                    Success(++count  + ", document import.");
                                }
                            }
                        }

                        break;
                    }
                case InsertOptionEnum.FromAnonymous:
                    {
                        var newDocument = new
                        {
                            firstName = "Ratomir",
                            lastName = "Vukadin",
                            bookmarks = new[]
                            {
                                new { url="https://try.dot.net/", name = "Try dot net" },
                                new { url="https://dotnetfiddle.net/", name = "Dot net fiddle" }
                            },
                            city = "TORONTO",
                            loc = new Point(-80.632504, 40.473298),
                            pop = 11981,
                            state = "OH"
                        };

                        Document createdNewDocument = await _repository.InsertDocument(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id), newDocument);
                        Success("New document successfully created.");
                        break;
                    }
                case InsertOptionEnum.FromModel:
                    {
                        EmailModel emailModel = new EmailModel()
                        {
                            Bcc = new List<string>() { "rosalee.fleming@enron.com" },
                            Cc = new List<string>() { "rosalee.fleming@enron.com" },
                            Ctype = "text/plain; charset=us-ascii",
                            Date = DateTime.Now,
                            Fname = "Ratomir",
                            Folder = "_inbox",
                            Fpath = "enron_mail_20110402/maildir/lay-k/_sent/81.",
                            Mid = "33068967.1075840285147.JavaMail.evans@thyme",
                            Recipients = new List<string>() { "katherine.brown@enron.com", "rosalee.fleming@enron.com", "rosalee.fleming@enron.com" },
                            Replyto = null,
                            Sender = "1_ratomirbl@bl18.com",
                            Subject = "Re: EXECUTIVE COMMITTEE MEETING - MONDAY, JULY 10",
                            Text = "Katherine:\n\nMr.Lay is planning on attending in person.\n\nTori\n\n\n\nKatherine Brown\n07 / 05 / 2000 01:15 PM\n\n\nTo: James M Bannantine / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Cliff \nBaxter / HOU / ECT@ECT, Sanjay Bhatnagar/ ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, \nRick Buy/ HOU / ECT@ECT, Richard Causey/ Corp / Enron@ENRON, Diomedes \nChristodoulou / SA / Enron@Enron, David W Delainey / HOU / ECT@ECT, James \nDerrick / Corp / Enron@ENRON, Andrew S Fastow / HOU / ECT@ECT, Peggy_Fowler @pgn.com, \nMark Frevert/ NA / Enron@Enron, Ben F Glisan / HOU / ECT@ECT, Kevin Hannon/ Enron \nCommunications @Enron Communications, David \nHaug / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Joe Hirko/ Enron \nCommunications @Enron Communications, Stanley Horton/ Corp / Enron@Enron, Larry L \nIzzo / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Steven J Kean / HOU / EES@EES, Mark \nKoenig / Corp / Enron@ENRON, Kenneth Lay/ Corp / Enron@ENRON, Rebecca P \nMark / HOU / AZURIX@AZURIX, Mike McConnell/ HOU / ECT@ECT, Rebecca \nMcDonald / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Jeffrey McMahon/ HOU / ECT@ECT, J \nMark Metts/ NA / Enron@Enron, Cindy Olson/ Corp / Enron@ENRON, Lou L \nPai / HOU / EES@EES, Ken Rice/ Enron Communications @Enron Communications, Jeffrey \nSherrick / Corp / Enron@ENRON, John Sherriff/ LON / ECT@ECT, Jeff \nSkilling / Corp / Enron@ENRON, Joseph W \nSutton / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Greg Whalley/ HOU / ECT@ECT, Thomas \nE White/ HOU / EES@EES, Brenda Garza-Castillo / NA / Enron@Enron, Marcia \nManarin / SA / Enron@Enron, Susan Skarness/ HOU / ECT@ECT, Stacy \nGuidroz / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Beena \nPradhan / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Karen K Heathman / HOU / ECT@ECT, \nSharron Westbrook/ Corp / Enron@ENRON, Kay Chapman/ HOU / ECT@ECT, Molly \nBobrow / NA / Enron@Enron, Rosane Fabozzi/ SA / Enron@Enron, Stephanie \nHarris / Corp / Enron@ENRON, Bridget Maronge/ HOU / ECT@ECT, Mary_trosper @pgn.com, \nNicki Daw/ NA / Enron@Enron, Inez Dauterive/ HOU / ECT@ECT, Carol Ann Brown / Enron \nCommunications @Enron Communications, Elaine \nRodriguez / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Nancy Young/ Enron \nCommunications @Enron Communications, Ann Joyner/ Corp / Enron@ENRON, Cindy \nStark / Corp / Enron@ENRON, Mary E Garza / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, \nMaureen McVicker/ HOU / EES@EES, Joannie Williamson/ Corp / Enron@ENRON, Rosalee \nFleming / Corp / Enron@ENRON, Vanessa Groscrand/ Corp / Enron@ENRON, Marsha \nLindsey / HOU / AZURIX@AZURIX, Cathy Phillips/ HOU / ECT@ECT, Loretta \nBrelsford / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Sue Ford/ HOU / ECT@ECT, Dolores \nFisher / NA / Enron@Enron, Karen Owens/ HOU / EES@EES, Dorothy Dalton/ Enron \nCommunications @Enron Communications, Jewel Meeks/ Enron Communications @Enron \nCommunications, Christina Grow/ Corp / Enron@ENRON, Lauren Urquhart/ LON / ECT@ECT, \nSherri Sera/ Corp / Enron@ENRON, Katherine Brown/ Corp / Enron@ENRON, Pam \nBenson / ENRON_DEVELOPMENT@ENRON_DEVELOPMENT, Jana Mills/ HOU / ECT@ECT, Liz M \nTaylor / HOU / ECT@ECT, Judy G Smith / HOU / EES@EES, Bobbie Power/ Corp / Enron@ENRON\ncc: Suzanne Danz/ Corp / Enron@ENRON, Videoconference @enron, Vanessa \nGroscrand / Corp / Enron@ENRON \nSubject: EXECUTIVE COMMITTEE MEETING - MONDAY, JULY 10\n\n\nExecutive Committee Weekly Meeting\nDate: Monday, July 10\nTime: 11:00 a.m. (CDT)\nLocation: 50th Floor Boardroom\nVideo: Connections will be established with remote locations upon request.\nConf call: AT & T lines have been reserved.  Please contact Sherri Sera \n(713 / 853 - 5984) \n or Katherine Brown(713 / 345 - 7774) for the weekly dial -in number and \npasscode.\n\nPlease indicate below whether or not you plan to attend this meeting and \nthrough what medium. \n\n Yes, I will attend in person _______\n\n By video conference from _______\n\n By conference call  _______\n\n No, I will not attend  _______\n\n * **\n\nPlease return this e - mail to me with your response by 12:00 p.m., Friday, \nJuly 7.\n\nThank you, \nKatherine\n\n",
                            To = new List<string>() { "katherine.brown@enron.com" }
                        };

                        Document newDocument = await _repository.InsertDocument(databaseName, collectionName.Id, emailModel);
                        emailModel.Id = newDocument.Id;
                        Success("New document successfully created.");
                        Info("New Email model id: " + emailModel.Id);

                        break;
                    }
                default:
                    break;
            }
        }

        public async Task UpdateDocument()
        {
            string databaseName = "";
            DocumentCollection collectionName = null;

            InsertCollAndDatabase(ref databaseName, ref collectionName);
            string id = ProgramHelper.EnterText("Enter document id");
            WorldBankModel model = await _repository.ReadDocumentByIdAsync<WorldBankModel>(id, UriFactory.CreateDocumentCollectionUri(databaseName, collectionName.Id));

            model.Status = "Inactive";

            Document updatedModel = await _repository.UpdateDocument<Document>(UriFactory.CreateDocumentUri(databaseName, collectionName.Id, model.Id), model);
            Success("Document with id >>> " + updatedModel.Id + " <<< updated.");
        }
    }
}
