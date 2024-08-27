.psx
.create "out.bin", 0x80000000


.org	0x80020228
j	0x80008300


.org	0x80008300


lw	s1,(v0)

la	at,0x87c5f803
beq	at,s1,hit

la	at,0x87c40804
nop
beq	at,s1,hit
la	at,0x8784080f
nop
beq	at,s1,hit
nop
la	at,0x11e1fe12
beq	at,s1,moon
nop
la	at,0x06e1fe1f
bne	at,s1,end
lw	s5,(v1)


la	at,0x8257c010
beq	s5,at,crate
nop
b 	end
nop
hit:
la	s1,0x8f04080a
b 	end
nop
moon:
la	s1,0x110a0e12
nop
b 	end
nop
crate:
la	s1,0x06e1fe00
end:
j	0x80020230
.close
