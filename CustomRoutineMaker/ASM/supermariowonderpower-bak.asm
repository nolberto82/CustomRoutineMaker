//.swi

.text

.org	0x0019cb48-0x4000
bl	main

//ecode:
//.dw	 0xe0000000
//evalue:
//.dw	 0x00000000


.org	0x02012b80

main:

adds    x8, x8, w9,uxtw#2
.word 	0xd000b0e9
ldr	x9,[x9,0xa98]
ldr	x9,[x9,0x20]
ldr	x9,[x9]
ldrh    w0,[x9,0x15f4]
ands	w0,w0,0x0040 //button a
beq	end


ldr	w11,[x8]

ldrh    w9,[x9,0x15f0]
ands	w10,w9,0x02 //button down
beq	up

sub	w11,w11,1
cmp	w11,0
ble	end
cmp	w11,5
bne	d7
mov	w11,3
d7:
cmp	w11,7
bne	store

mov	w11,6
b	store

up:
ands	w10,w9,0x01 //button up
beq	end
	
add	w11,w11,1
cmp	w11,13
bge	end
cmp	w11,4
bne	u7
mov	w11,6
u7:
cmp	w11,7
bne	store
mov	w11,8

store:
str	w11,[x8]

end:
ret



