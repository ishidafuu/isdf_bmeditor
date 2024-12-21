import { createApp } from 'vue'
import './style.css'
import App from './App.vue'
import router from './router.ts'
import { store } from './store'

window.addEventListener('error', function(event) {
    store.commit('errorHandler/setError', event.error);
});


createApp(App).use(router).use(store).mount('#app')