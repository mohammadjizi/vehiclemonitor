FROM microsoft/dotnet:2.1-sdk AS sdkImg
#set the current directory in the image (container) file system to src. create the folder if not exist.
WORKDIR /src/CustomerApi
#the below path for the source works if run from docker cli but might not work if run from inside project.
#since docker file and csproj are in the same directory, the path for the source should be like this.
#the destination is a folder in the image (container) file system.
COPY CustomerApi/CustomerApi.csproj .
#the below command will run from the new path where the csproj was copied.

RUN dotnet restore 
#copy all files into the src folder in the image (container) file system
COPY CustomerApi .

RUN dotnet publish CustomerApi.csproj -c Release -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runTimeImg
WORKDIR /app
#EXPOSE 49513
#EXPOSE 44317

EXPOSE 80

COPY --from=sdkImg /app .

ENTRYPOINT dotnet CustomerApi.dll
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet CustomerApi.dll