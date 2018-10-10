all : clean restore build

clean:
	dotnet clean src/

restore:
	dotnet restore src/

build:
	dotnet build src/
