<template>
  <v-container>
    <v-layout
      text-xs-center
      wrap
    >
    <v-app id="inspire">
        <v-content>
        <v-container
            class="fill-height"
            fluid
        >
            <v-row
            align="center"
            justify="center"
            >
            <v-col
                cols="12"
                sm="8"
                md="4"
            >
                <v-card class="elevation-12">
                <v-toolbar
                    color="primary"
                    dark
                    flat
                >
                    <v-toolbar-title>Login form</v-toolbar-title>
                    <v-spacer />
                </v-toolbar>
                <v-card-text>
                    <v-form>
                    <v-text-field
                        label="Correo"
                        v-model="email"
                        prepend-icon="person"
                        type="text"
                    />

                    <v-text-field
                        id="contraseña"
                        label="Contraseña"
                        v-model="contraseña"
                        prepend-icon="lock"
                        type="password"
                    />
                    </v-form>
                </v-card-text>
                <v-card-actions>
                    <v-spacer />
                    <v-btn color="primary" @click="login()">Login</v-btn>
                </v-card-actions>
                </v-card>
            </v-col>
            </v-row>
        </v-container>
        </v-content>
    </v-app>
    </v-layout>
  </v-container>
</template>

<script>
import axios from 'axios'
export default {
  name: 'login',
  data() {
    return {
      email: '',
      contraseña: '',
    }
  },
  methods: {
    login() {
        let me = this;
        axios.get('api/Usuario/'+me.email+'/'+me.contraseña)
        .then(function (response) {
            console.log(response.data)
            if (response.data) {
                localStorage.setItem('email',me.email)
                localStorage.setItem('contraseña',me.contraseña)
                localStorage.setItem('logeado',true)
                localStorage.setItem('id',response.data.id)
                window.top.location.href = "/facturas"
            }
        }).catch(function (error) {
            console.log(error);
        });
    }
  }
}
</script>
