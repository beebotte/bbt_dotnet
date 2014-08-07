Beebotte .Net SDK
===================

| what          | where                                  |
|---------------|----------------------------------------|
| overview      | http://beebotte.com/overview           |
| tutorials     | http://beebotte.com/tutorials          |
| apidoc        | http://beebotte.com/docs/restapi       |
| source        | https://github.com/beebotte/bbt_dotnet |

### Bugs / Feature Requests

Think you.ve found a bug? Want to see a new feature in beebotte? Please open an
issue in github. Please provide as much information as possible about the issue type and how to reproduce it.

    https://github.com/beebotte/bbt_dotnet/issues
    
## Install

Clone the source code from github
    git clone https://github.com/beebotte/bbt_dotnet.git
  
## Usage
To use the library, you need to be a registered user. If this is not the case, create your account at <http://beebotte.com> and note your access credentials.

As a reminder, Beebotte resource description uses a two levels hierarchy:

* Channel: physical or virtual connected object (an application, an arduino, a coffee machine, etc) providing some resources
* Resource: most elementary part of Beebotte, this is the actual data source (e.g. temperature from a domotics sensor)
  
### Beebotte Constructor
Use your account API and secret keys to initialize Beebotte connector:

    string accesskey  = “YOUR_API_KEY”;
    string secretkey  = “YOUR_SECRET_KEY”;
    string hostname   = “api.beebotte.com”;
    Connector bbt = new Connector( accesskey, secretkey, hostname);
    
### Reading Data
You can read data from one of your channel resources using:

    var records = bbt.Read("channel1", "resource1", 5); // read last 5 records
    
You can read data from a public channel by specifying the channel owner:

    var records  = bbtConnector.PublicRead("owner", "channel1", "resource1", 5); //read last 5 
    
### Writing Data
You can write data to a resource of one of your channels using:

    bbt.Write("channel1", "resource1", "Hello World");
   
If you have multiple records to write (to one or multiple resources of the same channel), you can use the `WriteBulk` method:

    var resources = new List<ResourceData>
                {
                    new ResourceData("resource1", "Hello"),
                    new ResourceData("resource2", "World")
                };
    bbtConnector.WriteBulk("channel1", resources);



