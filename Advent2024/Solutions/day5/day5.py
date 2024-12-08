
order_rules = []
updates = []
for line in open('input.txt').read().split('\n'):
    if "|" in line:
        order_rules.append(line.split("|"))
    
    if "," in line:
        updates.append(line.split(","))

#
toPrint = []
broken_updates = []
for update in updates:
    order_broken = False
    for rule in order_rules:
        before, after = rule
        if before not in update or after not in update:
            continue

        before_index = update.index(before)
        after_index = update.index(after)

        if after_index < before_index:
            order_broken = True
            broken_updates.append(update)
            break
    if not order_broken:
        toPrint.append(update)

mid_sum = sum([int(toPrint[x][int(len(toPrint[x])/2)]) for x in range(len(toPrint))])
print(mid_sum)

# Part 2
toPrint = []
for update in broken_updates:
    corrected_update = update[:]
    print(corrected_update)
    for rule in order_rules:
        before, after = rule
        if before not in update or after not in update:
            continue

        before_index = corrected_update.index(before)
        after_index = corrected_update.index(after)

        if after_index < before_index:
            new_index = min([corrected_update.index(a) for x, a in order_rules if x == before and a in corrected_update])

            corrected_update.pop(before_index)
            corrected_update.insert(new_index, before)
    print(corrected_update)
    toPrint.append(corrected_update)

mid_sum = sum([int(toPrint[x][int(len(toPrint[x])/2)]) for x in range(len(toPrint))])
print(mid_sum)