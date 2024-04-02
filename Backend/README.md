# LostAndFound - Backend
The backend of the "LostAndFound" system has a typical microservices architecture. It consists of API Gateway and four independent microservices: 
* Auth Service
* Chat Service
* Profile Service
* Publication Service


# Backend - Dependencies
Before you will be able to run backend solution of LostAndFound system you need to satisy few requirements.

## Requirements
<br />

### **Docker**
To get started with Docker, follow these steps:

1. Download and install Docker:
	* On Windows: Download the Docker Desktop for Windows installer and run it.
	* On Mac: Download the Docker Desktop for Mac installer and run it.
	* On Linux: Follow the instructions for your distribution from the Docker installation documentation.
	
2. Once Docker is installed, you can verify the installation by running the following command in a terminal:
	> docker --version

	This should print the version number of Docker that you have installed.

### **Position Stack Api**
Publication service communicate with external **Position Stack Api** in order to decode static addresses into geographical coordinates. PositionStack offers a straightforward and reliable solution for forward and reverse geocoding. The publication service contains configured API key for development and you don't need to generate your own API key if you don't want to. However, if you want to use your key, you need to do the following:


1. Get a FREE Api Key here:  https://positionstack.com/documentation

2. When you have the Api Key ready, you just need to update "appsettings.Development.json" files in all publication microservices. So you need to make the following changes:
	* Replace value for **PositionStackService:AccessKey** with the new Api Key. (It is by default set to paste-access-key)
		> Backend\PublicationService\src\LostAndFound.PublicationService\appsettings.Development.json

<br />

## Build and run in development
You can build and run backend solution using Docker Compose, following these steps:

1. Make sure you have Docker and Docker Compose installed on your machine. See the previous section for instructions on how to install Docker. Docker Compose is included with Docker Desktop on Mac and Windows. On Linux, you can install it using pip:
	> pip install docker-compose

2. Navigate to the backend directory:
	> cd Backend

3. Build and start the containers:
	> docker-compose up -d
	
	This will build the required Docker images and start the containers in the background.

	To stop the containers, execute the following command:
	> docker-compose down

4. Remember if you make changes to the code, you will need to rebuild the Docker image and restart the containers to see the changes.
To rebuild the image and restart the containers, you can use the following command:
	> docker-compose up --build -d

	Alternatively, you can rebuild the image and start the containers individually using the following commands:

	> docker-compose build
	
	> docker-compose up -d

	Either way, this will rebuild the image, recreate the containers, and start the containers with the new code.


## Service URLs
By default, services run at the following URLs:
* **API Gateway** - "http://localhost:5000/"
* **Auth Service** - "http://localhost:5100/"
* **Chat Service** - "http://localhost:5200/"
* **Profile Service** - "http://localhost:5300/"
* **Publication Service** - "http://localhost:5400/"

You can inspect each service swagger documentation using mentioned URLs.
