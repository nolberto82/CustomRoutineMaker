.nds
.arm
.create "out.bin", 0x0


.org	0x007b9508
bl	0x009ae990

.org	0x009ae990

push 	{r0-r7}

ldr 	r1, [r4, #0x2b8]
ldr 	r3, [r4, #0x2bc]
ldr 	r2, [r4, #0x528]
ands 	r6, r1, #0x100
beq 	end

ands 	r5, r3, #0x10
beq 	down

add 	r2, r2, #1
cmp 	r2, #8
movgt 	r2, #1
b 	store

down:
ands 	r5, r3, #0x20
beq 	end

sub 	r2, r2, #1
cmp 	r2, #1
movlt 	r2, #8

store:
ldr 	r1,=0x2c7c
str 	r2,[r4,r1]
add 	r1,r1,0x23c
str 	r2,[r4,r1]
str 	r2,[r4,0x528]
ldr	r7,=0xc61a64-0x9ae9ec-8
ldr	r7,[pc,r7]
ldr	r7,[r7]
str	r2,[r7,0x12c]
add	r1,r1,0x1c
mov	r2,1
str	r2,[r4,r1]
end:
pop 	{r0-r7}
ldr     r0,[r4,0x528]
bx lr

.pool
.close
