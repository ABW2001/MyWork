months = {1: "January", 2: "February", 3: "March", 4: "April", 5: "May", 6: "June", 7: "July", 8: "August",
          9: "September", 10: "October", 11: "November", 12: "December"}

while True:
    try:
        num = int(input("Enter a number between 1 and 12: "))
    except ValueError:
        print("Please enter numbers only")
    else:
        try:
            print(months[num])
        except:
            print("Please enter a number between 1 and 12")
        else:
            break
