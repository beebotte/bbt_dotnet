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

Nuget Install: https://www.nuget.org/packages/Beebotte.API.Server.Net

    Install-package Beebotte.API.Server.Net
    
Repository Cloning:

    git clone https://github.com/beebotte/bbt_dotnet.git
  
## Usage
To use the library, you need to be a registered user. If this is not the case, create your account at <http://beebotte.com> and note your access credentials.

As a reminder, Beebotte resource description uses a two levels hierarchy:

* Channel: physical or virtual connected object (an application, an arduino, a coffee machine, etc) providing some resources
* Resource: most elementary part of Beebotte, this is the actual data source (e.g. temperature from a domotics sensor)
  
### Beebotte Constructor
Use your account API and secret keys to initialize Beebotte connector:

    string accesskey  = "YOUR_API_KEY";
    string secretkey  = "YOUR_SECRET_KEY";
    string hostname   = "api.beebotte.com";
    Connector bbt = new Connector( accesskey, secretkey, hostname);
    
### Reading Data
You can read data from one of your channel resources using:

    var records = bbt.Read("channel1", "resource1", 5); // read last 5 records
    
You can read data from a public channel by specifying the channel owner:

    var records  = bbt.PublicRead("owner", "channel1", "resource1", 5); //read last 5 
    
### Writing Data
You can write data to a resource of one of your channels using:

    bbt.Write("channel1", "resource1", "Hello World");
   
If you have multiple records to write (to one or multiple resources of the same channel), you can use the `WriteBulk` method:

    var resources = new List<ResourceData>
        {
            new ResourceData("resource1", "Hello"),
            new ResourceData("resource2", "World")
        };
    bbt.WriteBulk("channel1", resources);

### Publishing Data
You can publish data to a channel resource using:

    bbt.Publish("any_channel", "any_resource", "Hello World")

Published data is transient. It will not be saved to any database; rather, it will be delivered to active subscribers in real time. 
The Publish operations do not require that the channel and resource be actually created. 
They will be considered as virtual: the channel and resource exist as long as you are publishing data to them. 
By default, published data is public, to publish a private message, you need to add `private-` prefix to the channel name like this:

    bbt.Publish("private-any_channel", "any_resource", "Hello World")

If you have multiple records to publish (to one or multiple resources of the same channel), you can use the `PublishBulk` method:

    var resources = new List<ResourceData>
        {
            new ResourceData("resource1", "Hello"),
            new ResourceData("resource2", "World")
        };
    bbt.PublishBulk("channel1", resources);

### Resource Management
The library provides a set of methods to manipulate resource objects as follows:

//Create the resource object

    var resource = new Resource("channel1", "resource1", "string");
    bbt.CreateResource(resource);    

//Get all resource objects for a given channel

    var resources = bbt.GetAllResources("channel1");
    
//Get a specific resource object

    var resource = bbt.GetResource("channel1", "resource1");
    
//Delete a resource object

    bbt.DeleteResource("channel1", "resource1");

### Channel Management
The library provides a set of methods to manipulate channel objects as follows:

//Create the channel object

    Channel channel = new Channel();
    channel.Name = "channel1";
    channel.Label = "label1";
    channel.Description = "description1";
    channel.IsPublic = false;
    var resources = new List<Resource>
    {
        new Resource("resource1","resource1", "resource 1", "string"),
        new Resource("resource2","resource2", "resource 2", "string")
    };
    channel.Resources = resources;
    bbt.CreateChannel(channel);

//Get all channel objects

    var channels = bbt.GetAllChannels();

//Get a specific channel object

    var channel = bbt.GetChannel("channel1");
    
After getting a channel, you can access the channel token as follows:

    var token = channel.Token;


//Delete a specific channel object

    bbt.DeleteChannel("channel1");

### Connection Management
The library provides a set of methods to manipulate connections as follows:

//Get all connections

    var connections = bbt.GetAllConnections<Beebotte.API.Server.Net.UserInfo>();

//Get connections for a given user

    var connections = bbt.GetUserConnections<Beebotte.API.Server.Net.UserInfo>("userId", "sessionId");

//Delete User connections

    bbt.DeleteConnection<Beebotte.API.Server.Net.UserInfo>("userId", "sessionId");

### IAM Token Management
The library provides a set of methods to manage IAM tokens as follows:

#### Create IAM token that allows to read data from a specific channel

       Connector bbt = new Connector(accesskey, secretkey, hostname);
       IAMToken readChannelDataToken = new IAMToken();
       var resources = new List<string>();
       resources.Add("Car.*");
       readChannelDataToken.Name = "Read_Car";
       readChannelDataToken.Description = "Read car channel data";
       var aclList = new List<ACL>();
       aclList.Add(new DataACL() { Action = DataACLTypes.DataRead.GetDescription(), Resources = resources });            
       readChannelDataToken.ACLList = aclList;
       var createdToken = bbt.CreateIAMToken(readChannelDataToken); 
       

#### Create IAM token that allows to read and write channels

    Connector bbt = new Connector(accesskey, secretkey, hostname);
    IAMToken writeChannelToken = new IAMToken();
    writeChannelToken.Name = "Write_Read_Channel";
    writeChannelToken.Description = "Write and Read Channel";
    var aclList = new List<ACL>();
    aclList.Add(new AdminACL() { Action = AdminACLTypes.ChannelWrite.GetDescription() });
    aclList.Add(new AdminACL() { Action = AdminACLTypes.ChannelRead.GetDescription() });
    writeChannelToken.ACLList = aclList;
    var createdToken = bbt.CreateIAMToken(writeChannelToken);

#### Get all IAM tokens

    var tokens = bbt.GetAllIAMTokens();
    
#### Delete an IAM token

    bbt.DeleteIAMToken("token_id");
    
#### Get IAM token given its ID

    var token = bbt.GetIAMToken("token_id");
    
    
## License
Copyright 2013 - 2017 Beebotte.

[The MIT License](http://opensource.org/licenses/MIT)
