import re
data = open('input.txt').read()

pattern = r'mul\((\d{1,3}),(\d{1,3})\)'
matches = re.findall(pattern, data)

multiplicationSum = sum([int(x) * int(y) for x, y in matches])
print(multiplicationSum)

#Part 2
sections = re.split(r"(don't\(\)|do\(\))", data)
enabledSum = 0
enabled = True
for section in sections:
    if section == "don't()":
        enabled = False
        continue

    if section == "do()":
        enabled = True
        continue
    
    if not enabled:
        continue

    matches = re.findall(pattern, section)
    enabledSum += sum([int(x) * int(y) for x, y in matches])

print(enabledSum)