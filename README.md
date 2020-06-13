# IMDB.NetCore.AzureCosmos
This is sample IMDB Movie Application which try to load the date from  theMovideDB and Appeneded to AZURE Cosmos Database

## The Movie DB Integration Details

API Key (v3 auth)
`f982f6f3e5c2bcb2de08fa020d39cd7d`

Example API Request

`https://api.themoviedb.org/3/movie/550?api_key=f982f6f3e5c2bcb2de08fa020d39cd7d`

API Read Access Token (v4 auth)


`eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmOTgyZjZmM2U1YzJiY2IyZGUwOGZhMDIwZDM5Y2Q3ZCIsInN1YiI6IjUyM2ZlNWY3NzYwZWUzNWNmMjA3ZDI3NiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.s4lx_QoGrigxrcYrmjzyRXw-DuGRtXIWpmTLnEb0dQo`



### Azure App Service Details

Rg: rg-azure-app-service-imdb-rg

name: azure-themoviedb.azurewebsites.net

###

Deployed URL

https://www.tutorialrepublic.com/codelab.php?topic=bootstrap&file=table-with-add-and-delete-row-feature



AZURE COSMOS DB Details:

Account Name : imdb-azure-cosmos


URL : https://imdb-azure-cosmos.documents.azure.com:443/

Primary Key : UwbWEZzKo8UurNdozP2JfCNZ4suLbno8EK48cboF1pwRToxC66OpMZlYf1FuOTCZ5rQac6YfObK4978NLLragA==

Primary Key Connection String : AccountEndpoint=https://imdb-azure-cosmos.documents.azure.com:443/;AccountKey=UwbWEZzKo8UurNdozP2JfCNZ4suLbno8EK48cboF1pwRToxC66OpMZlYf1FuOTCZ5rQac6YfObK4978NLLragA==;

Database : IMDB