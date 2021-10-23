import sys
import time
import requests
import json

f = "./output.wav"
js = open('assembly_key.json')
key = json.load(js)

def read_file(f, chunk_size=5242880):
    with open(f, 'rb') as _file:
        while True:
            data = _file.read(chunk_size)
            if not data:
                break
            yield data

headers = {'authorization': key[0]['key']} # convert to json element
response = requests.post('https://api.assemblyai.com/v2/upload',
                        headers=headers,
                        data=read_file(f))

print(response.json())

headers= {'authorization': "bddb069917184c3ea468ac44a52f9879", 
        'content-type': 'application/json'}
endpoint = "https://api.assemblyai.com/v2/transcript"
json = {"audio_url": response.json()['upload_url'], "punctuate":False}
response = requests.post(endpoint, json=json, headers=headers)
print(response.json())
state = response.json()
endpoint = endpoint + "/" + state['id']
# time.sleep(30)
while (state['status'] != 'completed'):
    response = requests.get(endpoint, headers=headers)
    # time.sleep(1)
    state = response.json()
    print(state['status'])

print(state['text'])
    