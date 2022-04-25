while True:
    sentence = input("Enter a sentence of at least five words:\n")
    words = sentence.split()
    if len(words) > 4:
        break
    else:
        print("The sentence must consist of at least five words")

sentence = ""
for i in range(0, len(words)):
    sentence = sentence + words[len(words) - i - 1] + " "

print(sentence)
words = sentence.split()
sentence = ""

for i in range(0, len(words)):
    word = words[i]
    for j in range(0, len(word)):
        sentence = sentence + word[len(word) - 1 - j]
    sentence = sentence + " "

print(sentence)
