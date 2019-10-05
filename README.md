# GomokuQQ
一个qq群用的双人在线五子棋bot<br>
**你甚至可以在QQ群里下五子棋**
<br>

## How to deploy?

### Requirement: 
- visual studio 2019 (never tried 2017 but maybe it's ok)
- read [the documentation of newbe.mahua qq bot framework](http://www.newbe.pro/2018/06/10/Newbe.Mahua/Begin-First-Plugin-With-Mahua-In-v1.9/) **IN DETAIL AND DO AS HE SAID**
- .net framework 4.5.2 or above
- a server with windows os, or you could try deploy it on linux by following [this link](https://github.com/CoolQ/docker-wine-coolq)

### Steps
1. `clone` the project to your server/PC
2. open visual studio and build the project (you may need to restore the nuget pkgs)
3. run `senrenbanka.murasame.qqbot/build.bat`, if build successed you will see "BUILD SUCCESSED" displayed on the terminal
4. cd to `bin/Debug/CQP` folder
5. move all files under the folder to your CoolQ install path
6. open CoolQ, click the version number at the lower right for **5 TIMES**, turn on developer mode and restart CoolQ
7. select `lovemurasame` in CoolQ's application management and open it
8. type `/help` in group, read the commands/rules and start playing!
