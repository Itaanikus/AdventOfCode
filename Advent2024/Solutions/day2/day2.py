safeReports = 0

with open('input.txt', 'r') as file:
# with open('example.txt', 'r') as file:
    for line in file:
        levels = [int(line) for line in line.split()]
        
        if levels[0] == levels[1]:
            #report not safe
            continue

        increasing = levels[0] < levels[1]
        for i in range(len(levels)):
            if i == 0:
                continue

            levelDiff = levels[i] - levels[i-1]
            if increasing and (levelDiff < 1 or levelDiff > 3):
                break
            if not increasing and (levelDiff > -1 or levelDiff < -3):
                break

            if i+1 == len(levels):
                safeReports += 1
print(safeReports)

#Part 2
# Stolen for inspiration :'(
def is_safe(row):
    inc = [row[i + 1] - row[i] for i in range(len(row) - 1)]
    if set(inc) <= {1, 2, 3} or set(inc) <= {-1, -2, -3}:
        return True
    return False

data = [[int(y) for y in x.split(' ')] for x in open('input.txt').read().split('\n')]

safeReports = sum([any([is_safe(row[:i] + row[i + 1:]) for i in range(len(row))]) for row in data])
print(safeReports)