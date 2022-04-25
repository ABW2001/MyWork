import requests
myRequest = requests.post("http://127.0.0.1:5000/", data={'myself': 'Hello!'}) # Send request to server
print(myRequest.text) # displays the result body, after receiving the response from the server