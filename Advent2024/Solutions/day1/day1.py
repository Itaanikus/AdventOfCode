column1 = []
column2 = []

with open('day1_example.txt', 'r') as file:
    for line in file:
        columns = line.split()

        column1.append(int(columns[0]))
        column2.append(int(columns[1]))

column1.sort()
column2.sort()

print("Column 1:", column1)
print("Column 2:", column2)
