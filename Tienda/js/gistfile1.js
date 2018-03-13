/*		Validación y llamada AJAX para enviar el correo */
		$(".boton_envio").click(function() {
	 
			var nombre = $(".nombre").val();
				email = $(".email").val();
				validacion_email = /^[a-zA-Z0-9_\.\-]+@[a-zA-Z0-9\-]+\.[a-zA-Z0-9\-\.]+$/;
				asunto = $(".asunto").val();
				mensaje = $(".mensaje").val();
	 
			if ((nombre == "") || (nombre == $(".nombre").attr('placeholder'))){
				$(".nombre").focus();
				$('.msg').text('Falta su nombre').addClass('msg_error').animate({ 'right' : '140px' }, 300);
				return false;
			} else if(email == "" || (email == $(".email").attr('placeholder')) || !validacion_email.test(email)){
				$(".email").focus();
				if (!validacion_email.test(email))
					$('.msg').text('Email no válido').addClass('msg_error').animate({ 'right' : '140px' }, 300);
				else
					$('.msg').text('Falta su email').addClass('msg_error').animate({ 'right' : '140px' }, 300);
				return false;
			} else if(asunto == "" || (asunto == $(".asunto").attr('placeholder'))){
				$(".asunto").focus();
				$('.msg').text('Falta el asunto').addClass('msg_error').animate({ 'right' : '140px' }, 300);
				return false;
			} else if(mensaje == ""){
				$(".mensaje").focus();
				$('.msg').text('No hay mensaje').addClass('msg_error').animate({ 'right' : '140px' }, 300);
				return false;
			} else {
					// Eliminamos la clase del mensaje de error si existiera
					if($('.msg').hasClass('msg_error')) $('.msg').removeClass('msg_error');
					// Si todo paso, aqui ira la llamada AJAX
					$('.ajaxgif').removeClass('hide');
					var datos = 'nombre='+ nombre + '&email=' + email + '&asunto=' + asunto + '&mensaje=' + mensaje;
					$.ajax({
						type: "POST",
						url: "../proceso.php",
						data: datos,
						success: function() {
							$('.ajaxgif').hide();
							$('.msg').text('Mensaje enviado!').addClass('msg_ok').animate({ 'right' : '140px' }, 300);
						},
						error: function() {
							$('.ajaxgif').hide();
							$('.msg').text('Hubo un error!').addClass('msg_error').animate({ 'right' : '140px' }, 300);
						}
					});
					return false;
				}
	 
		});
/*		Detectamos que si ha habido un error en el formulario
		el mensaje de error desaparezca. La clase del mensaje de error 
		la eliminamos cuando el formulario esté completo e intentemos
		enviarlo. */
		$(".nombre,.email,.asunto,.mensaje").on({
			click:function() {
				if($('.msg').hasClass('msg_error')) $('.msg').animate({ 'right' : '-140px' }, 300);
			},
			keypress:function() {
				if($('.msg').hasClass('msg_error')) $('.msg').animate({ 'right' : '-140px' }, 300);
			}
		});
	});
