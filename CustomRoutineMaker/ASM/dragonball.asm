.gba
.thumb
.create "out.bin",0x0203ff00
.definelabel addr,0x08014ef8
.definelabel jump,0x0203ff00


.org	addr
ldr	r3,=jump+1
bx	r3
.pool


.org	jump
asr	r0,r0,0x10

mov	r3,r6
sub	r3,12	
ldrb	r1,[r3]
mov 	r2,1
and	r1,r2
beq	end

mov 	r0,0xfa
lsl	r0,4
strh	r0,[r7,0x2e]

end:
ldrh	r2,[r7,0x36]
mov	r3,0x36
ldsh	r2,[r7,r3]
ldr	r5,=addr+9
bx	r5

.pool
.close
