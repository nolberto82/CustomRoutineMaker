.nds
.arm
.create "out.bin", 0x0


.org	0x007a2990
bl	0x009ae990

.org	0x009ae990


push 	{r0-r7}
ldr 	r1,[r4,#0x2b8]
ldr 	r3,[r4,#0x2bc]
ldr 	r2,[r4,#0x528]
ands 	r6,r1,#0x100
beq 	end

ands 	r7,r3,0x10
beq 	next

add 	r2,r2,#1
cmp 	r2,#8
movgt 	r2,#1
b 	store

next:
ands 	r7,r3,0x20
beq 	end

sub 	r2,r2,#1
cmp 	r2,#1
movlt 	r2,#8

store:
mov	r6,1
str	r6,[r5,0x20]
mov	r6,0x1e
str	r6,[r5,0x0c]
str 	r2,[r4,0x528]
sub	r0,r0,0x258
str 	r2,[r4,r0]
add	r0,r0,0x23c
str 	r2,[r4,r0]

end:
pop 	{r0-r7}
mov 	r5,0
bx 	lr

.pool
.close
