.gba
.create "out.bin", 0x00000000
.definelabel branch,0x08000000
.definelabel function,0x0203ff00


.org	branch
ldr	r0,=function+1
bx	r0
.pool

.org	function

ldr	r0,=branch+9
push	{r0}

ldrh	r1,[r2,2]

pop	{pc}

.pool
.close
