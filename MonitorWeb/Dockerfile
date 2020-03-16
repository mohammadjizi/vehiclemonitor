FROM microsoft/dotnet:2.1-sdk AS sdkImg
#set the current directory in the image (container) file system to src. create the folder if not exist.
WORKDIR /src/MonitorWeb
#the below path for the source works if run from docker cli but might not work if run from inside project.
#since docker file and csproj are in the same directory, the path for the source should be like this.
#the destination is a folder in the image (container) file system.
COPY MonitorWeb/MonitorWeb.csproj .
#the below command will run from the new path where the csproj was copied.

RUN dotnet restore 
#copy all files into the src folder in the image (container) file system
COPY MonitorWeb .

RUN dotnet publish MonitorWeb.csproj -c Release -o /app


#Angular build
FROM node as nodebuilder

# set working directory
#RUN mkdir /usr/src/app
WORKDIR /usr/src/app

# add `/usr/src/app/node_modules/.bin` to $PATH
#ENV PATH /usr/src/app/node_modules/.bin:$PATH


# install and cache app dependencies
COPY MonitorWeb/ClientApp/package.json /usr/src/app/package.json
RUN npm install
#RUN npm install -g @angular/cli@1.7.0 --unsafe

# add app

COPY MonitorWeb/ClientApp/. /usr/src/app

RUN npm run build

#End Angular build

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runTimeImg
WORKDIR /app

EXPOSE 80

COPY --from=sdkImg /app .
RUN mkdir -p /app/ClientApp/dist
COPY --from=nodebuilder /usr/src/app/dist/. /app/ClientApp/dist/
ENTRYPOINT dotnet MonitorWeb.dll
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet MonitorWeb.dll

