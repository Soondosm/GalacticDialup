# this is a test file isolated from Unity entirely. 
# The aim is to make sure that assembly AI works alone, then plug it into Unity.

import speech_recognition as s_r
import numpy as np
import pyaudio
import wave
Format = pyaudio.paInt16
Channels = 1
Record_Duration = 5
output_path = "output.wav"
FPB = 1024
Rate = 44100

p = pyaudio.PyAudio()

stream = p.open(format=Format, channels=Channels, rate=Rate, input=True,
frames_per_buffer=FPB)


s_r.Microphone.list_microphone_names()
print(s_r.Microphone.list_microphone_names()) #print all the microphones connected to your machine

print("     -RECORDING")
frames = []

for i in range(0, int(Rate / FPB * Record_Duration)):
    data = stream.read(FPB)
    frames.append(data)

print("        -DONE")
stream.stop_stream()
stream.close()
p.terminate()

wf = wave.open(output_path, 'wb')
wf.setnchannels(Channels)
wf.setsampwidth(p.get_sample_size(Format))
wf.setframerate(Rate)
wf.writeframes(b''.join(frames))
wf.close()