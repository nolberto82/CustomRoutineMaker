.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08007300
.definelabel function,0x0203ff00


.org	branch
ldr	r1,=function+1
bx	r1
.pool

.org	function

ldr	r1,=branch+9
push	{r1}

push	{r2}

add	r1,r5,0
add	r1,0x48
mov	r2,0
sub	r2,15
strh	r2,[r1]

pop	{r2}

add r0,r0, r2
ldr r0,[r0,0x00]
mov r1,0x20
and r0,r1

pop	{pc}

.pool
.close
