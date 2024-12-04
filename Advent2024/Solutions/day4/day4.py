data = [[y for y in x] for x in open('input.txt').read().split('\n')]

def spells_xmas(char1, char2, char3, char4):
    if char1 == 'X' and char2 == 'M' and char3 == 'A' and char4 == 'S':
        return True
    if char1 == 'S' and char2 == 'A' and char3 == 'M' and char4 == 'X':
        return True
    return False
    

xmasCount = 0
for x in range(len(data)):
    xmasCount += sum([spells_xmas(data[x][y], data[x][y+1], data[x][y+2], data[x][y+3]) for y in range(len(data[x]) - 3)])
    
    if x < len(data) - 3:
        xmasCount += sum([spells_xmas(data[x][y], data[x+1][y], data[x+2][y], data[x+3][y]) for y in range(len(data[x]))])
        xmasCount += sum([spells_xmas(data[x][y], data[x+1][y+1], data[x+2][y+2], data[x+3][y+3]) for y in range(len(data[x]) - 3)])
    if x > 2:
        xmasCount += sum([spells_xmas(data[x][y], data[x-1][y+1], data[x-2][y+2], data[x-3][y+3]) for y in range(len(data[x]) - 3)])
    
print(xmasCount)

# Part 2

x_masCount = 0
for x in range(1, len(data) - 1):
    for y in range(1, len(data[x]) - 1):
        if data[x][y] == 'A':
            crossPart1 = data[x-1][y-1] + data[x][y] + data[x+1][y+1]
            crossPart2 = data[x-1][y+1] + data[x][y] + data[x+1][y-1]

            if (crossPart1 == 'SAM' or crossPart1 == 'MAS') and (crossPart2 == 'SAM' or crossPart2 == 'MAS'):
                x_masCount += 1

print(x_masCount)