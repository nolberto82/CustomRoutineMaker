.gba
.create "out.bin", 0x00000000
.definelabel branch,0x080243ac
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

add	r2,0xd8
str	r2,[sp,0x14]

ldr	r1,=branch+9
push	{r1}

ldr	r1,=0xfffffb00
str	r1,[r7,0x60]

ldr	r1,[r2]
add	r0,r7,0


pop	{pc}

.pool
.close
