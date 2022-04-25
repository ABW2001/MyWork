import bitmex
import json
import threading
from datetime import datetime
from colorama import Fore, Back, Style
import winsound


class Tick:
    def __init__(self, time, price):
        self.time = time
        self.price = price


class PriceChecker:
    def __init__(self):
        self.levelsList = []
        self.currentPrice = 0.0
        self.BitmexClient = bitmex.bitmex(test=False)
        self.previousPrice = 0.0

    @property
    def previousPrice(self):
        return self.__previousPrice
    @previousPrice.setter
    def previousPrice(self, newValue):
        self.__previousPrice = newValue
    @property
    def levelsList(self):
        return self.__levelsList
    @levelsList.setter
    def levelsList(self, newValue):
        self.__levelsList = newValue
    @property
    def currentPrice(self):
        return self.__currentPrice
    @currentPrice.setter
    def currentPrice(self, newValue):
        self.__currentPrice = newValue
    # Class Methods
    # =============

    def displayList(self):
        print(chr(27) + "[2J")
        print("Price Levels In The List")
        print("========================")
        for i in range(0, len(self.levelsList)):
            for j in range(0, len(self.levelsList)):
                if self.levelsList[i] > self.levelsList[j]:
                    temp = self.levelsList[i]
                    self.levelsList[i] = self.levelsList[j]
                    self.levelsList[j] = temp

        for i in range(0, len(self.levelsList)):
            print("${:,.2f}".format(self.levelsList[i]))

    # Display the menu and get user input about what methods to execute next
    def displayMenu(self):
        min = 0
        max = 5
        errorMsg = "Please enter a valid option between " + str(min) + " and " + str(max)

        print("MENU OPTIONS")
        print("============")
        print("1. Add a price level")
        print("2. Remove a price level")
        print("3. Remove all price levels")
        if self.currentPrice > 0:
            print("4. Display the current Bitcoin price here: " + "${:,.2f}".format(self.currentPrice))
        else:
            print("4. Display the current Bitcoin price here")
        print("5. Start the monitoring")
        print("0. Exit the program")
        print(" ")

        # Get user input. Keep on requesting input until the user enters a valid number between min and max 
        selection = 99
        while selection < min or selection > max:
            try:
                selection = int(input("Please enter one of the options: "))
            except:
                print(errorMsg) # user did not enter a number
                continue # skip the following if statement
            if selection < min or selection > max:
                print(errorMsg) # user entered a number outside the required range
        return selection # When this return is finally reached, selection will have a value between (and including) min and max

    def addLevel(self):
        try:
            value = float(input("Input a float value: "))
            self.levelsList.append(value)
        except:
            print("Please input a float value")

    def removeAllLevels(self):
        self.levelsList.clear()

    def readLevelsFromFile(self):
        try:
            self.levelsList.clear()
            file = open("cryptoFile.txt", "r")
            while True:
                value = file.readline()
                value = value.rstrip()
                if value == '':
                    break
                self.levelsList.append(float(value))
            file.close()
        except:
            print("An error occurred while handling the file")
            return

    def writeLevelsToFile(self):
        file = open("cryptoFile.txt", "w")
        for i in self.levelsList:
            i = str(i) + "\n"
            file.write(i)
        file.close()

    def removeLevel(self):
        try:
            value = float(input("Input a float value: "))
            self.levelsList.remove(value)
        except:
            print("Please input a float value")

    def removeAllLevels(self):
        self.levelsList.clear()

    def updateMenuPrice(self):
        tickobj = self.getBitMexPrice()
        self.currentPrice = tickobj.price

    #Function: call the Bitmex Exchange
    def getBitMexPrice(self):
        # send a request to the exchange for bitcoins data in USD 'XBTUSD'
        # The Json response is converted into a tuple which we name responseTuple.
        responseTuple = self.BitmexClient.Instrument.Instrument_get(filter=json.dumps({'symbol': 'XBTUSD'})).result()
        # The tuple consists of the bitcoin information (in the form of a dictionary with key>value pairs) plus
        # some additional meta data received from the exchange.
        # Extract only the dictionary (Bitcoin information) from the tuple.
        responseDictionary = responseTuple[0:1][0][0]
        # create a Tick object and set its variables to the timestamp and lastPrice data from the dictionary.
        return Tick(responseDictionary["timestamp"], responseDictionary['lastPrice'])

    def monitorLevels(self):
        threading.Timer(5.0, self.monitorLevels).start()
        self.previousPrice = self.currentPrice
        tickObj = self.getBitMexPrice()
        self.currentPrice = tickObj.price
        if self.previousPrice == 0.0:
            self.previousPrice = self.currentPrice
        print("")
        print("Price Check at " + str(datetime.now()) + "   (Press Ctrl+C to stop the monitoring)")
        print("=================================================================================")
        displayList = []
        for price in self.levelsList:
            priceLevelLabel = 'Price Level:     ' + str(price)
            sublist = [priceLevelLabel, price]
            displayList.append(sublist)

        previousPriceLabel = Fore.LIGHTYELLOW_EX + Back.BLUE + 'Previous Price:   ' + str(self.previousPrice) + '\x1b[0m'
        sublist = [previousPriceLabel, self.previousPrice]
        displayList.append(sublist)
        if self.currentPrice > self.previousPrice:
            currentPriceLabel = Fore.LIGHTYELLOW_EX + Back.GREEN + 'Current Price:     ' + str(self.currentPrice) + '\x1b[0m'
        elif self.currentPrice < self.previousPrice:
            currentPriceLabel = Fore.LIGHTYELLOW_EX + Back.RED + 'Current Price:     ' + str(self.currentPrice) + '\x1b[0m'
        else:
            currentPriceLabel = Fore.LIGHTYELLOW_EX + Back.BLUE + 'Current Price:     ' + str(self.currentPrice) + '\x1b[0m'
        sublist = [currentPriceLabel, self.currentPrice]
        displayList.append(sublist)
        displayList.sort(key=lambda y: y[1])

        for x in displayList:
            print(x[0])
        print(Style.RESET_ALL)

        for x in displayList:
            if "Price Level:" in x[0]:
                priceLevel = x[1]
                if priceLevel == self.previousPrice or priceLevel == self.currentPrice \
                        or (self.previousPrice < priceLevel < self.currentPrice) \
                        or (self.previousPrice > priceLevel > self.currentPrice):
                    if self.currentPrice > self.previousPrice:
                        frequency = 800
                        duration = 700
                    else:
                        frequency = 400
                        duration = 700
                    winsound.Beep(frequency, duration)
                    print(Back.GREEN + "Alarm" + '\x1b[0m')
        checkerObj.updateMenuPrice()


checkerObj = PriceChecker()
checkerObj.readLevelsFromFile()
userInput = 99
while userInput != 0:
    checkerObj.displayList()
    userInput = checkerObj.displayMenu()
    if userInput == 1:
        checkerObj.addLevel()
        checkerObj.writeLevelsToFile()
    elif userInput == 2:
        checkerObj.removeLevel()
        checkerObj.writeLevelsToFile()
    elif userInput == 3:
        checkerObj.removeAllLevels()
        checkerObj.writeLevelsToFile()
    elif userInput == 4:
        checkerObj.updateMenuPrice()
    elif userInput == 5:
        userInput = 0
        checkerObj.monitorLevels()
