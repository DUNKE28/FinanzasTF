<template>
<v-layout align-center>
    <v-flex>
        <v-toolbar flat color="white">
            <v-toolbar-title>Registro</v-toolbar-title>
            <v-divider class="mx-2" inset vertical></v-divider>
            <v-spacer></v-spacer>        
        </v-toolbar>
        <v-container align="center" grid-list-sm class="pa-4 white">
            <h3>Ingrese los siguiente campos:</h3>
            <v-layout row wrap>
                <v-flex xs12 sm4 md4 lg4 xl4>
                    <v-text-field :rules="[ v => !!v || 'La razon social es requerida']" v-model="razonSocial" label="Razon Social">
                    </v-text-field>
                </v-flex>
            </v-layout>
            <v-flex xs12 sm4 md4 lg4 xl4>
                <v-text-field :rules="[ v => !!v || 'Ruc es requerido']"  v-model="ruc" label="RUC">
                </v-text-field>
            </v-flex>
            <v-flex xs12 sm4 md4 lg4 xl4>
                <v-text-field :rules="[ v => !!v || 'El correo es requerido']" v-model="email" label="Correo electronico">
                </v-text-field>
            </v-flex>
            <v-flex xs12 sm4 md4 lg4 xl4>
                <v-text-field :rules="[ v => !!v || 'La contraseña es requerida']" v-model="contraseña" type='password' label="Contraseña">
                </v-text-field>
            </v-flex>
            <v-flex xs12 sm4 md4 lg4 xl4>
                <v-text-field :rules="[ v => (v==this.contraseña | v=='') || 'La contraseña no coincide']" v-model="confirmContraseña" type='password' label="Repetir contraseña">
                </v-text-field>
            </v-flex>
        </v-container>
        <v-container grid-list-sm class="pa-4 white">
            <v-flex xs12 sm12 md12 lg12 xl12>
                <v-btn @click="limpiar()" color="blue darken-1" flat>Limpiar</v-btn>
                <v-btn @click="guardarVeri()" color="success">Registrarse</v-btn>
            </v-flex>
        </v-container>
    </v-flex>
</v-layout>
</template>

<script>
import axios from 'axios'

export default {
    data() {
        return {
            razonSocial: '',
            ruc: '',
            email: '',
            contraseña: '',
            confirmContraseña: '',
        }
    },

    watch: {
        dialog(val) {
            val || this.close()
        }
    },

    created() {

    },

    methods: {

        guardar() {
            let me = this;
            axios
                .post("api/Usuario", {
                    razonSocial: me.razonSocial,
                    ruc: me.ruc,
                    email: me.email,
                    contraseña: me.contraseña,
                })
                .then(function (response) {
                    if(response.data){
                        window.top.location.href = "/login"
                    }                
                })
                .catch(function (error) {
                    console.log(error);
                });
        },

        guardarVeri(){
            if (this.razonSocial != '' && this.razonSocial != '' && this.email != '' && this.contraseña != '' && (this.contraseña == this.confirmContraseña)){
                this.guardar()
            }
        },

        limpiar() {
            this.razonSocial = '',
            this.ruc = '',
            this.email = '',
            this.contraseña = '',
            this.confirmContraseña = ''
        },
    },

}
</script>