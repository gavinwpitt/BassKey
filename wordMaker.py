"""
Gavin Pitt
Markov Chain
"""

import sys
import random
import os

class Markov(object):

	"""
	Initializes the 'Markov' object.
	Expects to have 'words' handed to it, which is the corpus for sentences to generate randomly.
	I set it up to expect words and not a file name because I want the program to be able to open multiple files.
	"""
	def __init__(self, words):
		self.words = words
		self.wordCount = len(self.words)
		self.dictionary = {}
		self.createDictionary()

	def triples(self):
		if len(self.words) < 3:
			return

		for i in range(self.wordCount - 2):
			yield (self.words[i], self.words[i+1], self.words[i+2])

	"""
	Creates the dictionary, and is called in the class Initialization.
	Takes a set of three words, and creates a tuple key out of the first two words.
	The third word is added as the value to the key; if the key already exists it is added as another possible value.
	"""
	def createDictionary(self):
		for word0, word1, word2 in self.triples():
			key = (word0, word1)
			if key in self.dictionary:
				self.dictionary[key].append(word2)
			else:
				self.dictionary[key] = [word2]


	def generateSentence(self, limit):
		if self.wordCount <= 3:
			return "Not enough input (wordCount < 3"
		random.seed(None)
		randInt = random.randrange(0, self.wordCount - 3)
		word0, word1 = self.words[randInt], self.words[randInt + 1]
		sentence = []
		sentence.append(word0)
		if limit > 0:
			for x in range(0,limit):
				if (word0, word1) in self.dictionary:
					word0, word1 = word1, random.choice( self.dictionary[ (word0, word1) ] )
					sentence.append(word0)
				else:
					break
		else:
			while (word0, word1) in self.dictionary:
				word0, word1 = word1, random.choice( self.dictionary[ (word0, word1) ] )
				sentence.append(word0)

		return ' '.join(sentence)

def main():

	if len(sys.argv) > 1:
		path = "./" + sys.argv[1] + "/"
		if not os.path.exists(path):
			print("Path does not exists!")
			print("Please enter valid folder name within this directory!")
			return
			if not os.path.isdir(path):
				print("Given folder name is not a valid folder!")
				return
	else:
		path = "./TextFiles/"

	words = []
	for filename in os.listdir(path):
		print("Reading " + filename)
		f = open(path + filename, 'r')
		words += f.read().split()
	
	markovGenerator = Markov(words)

	print markovGenerator.generateSentence(25)

main()