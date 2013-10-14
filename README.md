Youtube Subscription Manager
-------

Tool to manage my Youtube subscription feed a bit better. I probably
go through over 200 videos daily, so this helps a bit in getting the 
videos I want.

Copy the source code from your My Subscriptions page on Youtube. You'll
want to do this via the Developer Tools 'inspect' tool, rather than 
right-clicking on View Page Source. 

Youtube dynamically adds more recent subscriptions as your scroll down
your My Subscriptions feed, so the extra videos beyond the first 30
aren't visible. ~30 videos is manageable, but after I hit ~200 
new videos, I prefer to hold some off for later. However, due to the 
200 video limit Youtube has, this isn't possible without somehow saving
the data. Hence this application.

1. Copy the source code over to a local HTML file.
2. Open the ytSubscriber.exe application and point it to the local HTML file. 
3. Filter your subscriptions, or click on a video to open the video in your default web browser.

This was a fun tool to build, all the more easier because of a lot of 
work using the MVVM paradigm on Windows Phone C# applications. 

Working on a way to merge the .dll library files and .exe into a 
single .exe file, but the default ILMerge tool does not support WPF 
applications, so it requires extra hackery which I'm not willing to 
employ. So for now, the application only works if the entire 
Release\ folder is provided, with all the library resources in the 
same directory.