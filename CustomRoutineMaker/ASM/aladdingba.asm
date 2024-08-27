.gba
.create "out.bin", 0x00000000
.definelabel branch,0x080182d0
.definelabel function,0x0203ff00


.org	branch
ldr	r1,=function+1
bx	r1
.pool

.org	function

ldr	r1,=branch+9
push	{r1}

add	r1,r6,0
sub	r1,0x0c
ldrb	r1,[r1]
mov	r2,0x80
and	r1,r2
beq	end

mov	r2,0
sub	r2,r2,6
lsl	r0,r2,8

end:
str 	r0,[r4,0x30]
add 	r0,r4,0x00
add 	r0,0x33
ldrb 	r1,[r0,0x00]



pop	{pc}

.pool
.close
