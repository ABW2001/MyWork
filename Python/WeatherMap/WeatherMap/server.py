from flask import Flask, request, jsonify
import requests, json
from flask_cors import CORS, cross_origin

url = "https://api.tomorrow.io/v4/timelines"
querystring = {
    "location": "33, -84",
    "timezone": "Africa/Johannesburg",
    "fields": ["temperature", "cloudCover", "windSpeed", "windDirection"],
    "units": "metric",
    "timesteps": "1h",
    "apikey": ""}  # Insert your tomorrow.io apikey here

app = Flask(__name__)
CORS(app)


@app.route('/', methods=['POST'])
@cross_origin(origin='*')
def result():
    mapData = json.loads(request.form['mapData'])
    coords = str(mapData['lat']) + ", " + str(mapData['lng'])
    results = weather(coords)
    results = json.dumps(results)
    return results  # send response to client (browser or IDE)


def weather(coords):
    querystring["location"] = coords
    response = requests.request("GET", url, params=querystring)
    counter = 0
    t = response.json()['data']['timelines'][0]['intervals'][0]['values']['temperature']
    results = response.json()['data']['timelines'][0]['intervals']
    for hourlyResult in results:
        counter = counter + 1
        date = hourlyResult['startTime'][0:10]
        time = hourlyResult['startTime'][11:19]
        wind = hourlyResult['values']['windSpeed']
        temp = round(hourlyResult['values']['temperature'])
        cloud = hourlyResult['values']['cloudCover']
        print("On", date, "at", time, "it will be", temp, "C", "with", wind, "m/s wind", "cloudCover:", cloud)
        if counter == 12:
            break
    return results[0:12]

# How to run this server in the background:
# =========================================
# In the terminal (Command Prompt), navigate to the folder containing your server.py app.
# Type the following command to inform Flask to use the server.py app: set FLASK_APP=server
# Type the following command to start Flask: flask run
# After using the server, Press Ctrl+C to stop it.
