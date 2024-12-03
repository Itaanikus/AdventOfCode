leftColumn = []
rightColumn = []

with open('day1_input.txt', 'r') as file:
# with open('day1_example.txt', 'r') as file:
    for line in file:
        columns = line.split()

        leftColumn.append(int(columns[0]))
        rightColumn.append(int(columns[1]))

leftColumn.sort()
rightColumn.sort()

diffSum = 0

for i in range(len(leftColumn)):
    diffSum += abs(leftColumn[i] - rightColumn[i])

print(diffSum)

# Part 2
similarityScore = 0
for value in leftColumn:
    similarityScore += value * rightColumn.count(value)

print(similarityScore)