<template>
  <div v-if="latestError">
    <div class="error-box">
      <p>Error Occurred:</p>
      <p>{{ latestError }}</p>
      <button @click="clearError">Clear Error</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const latestError = computed(() => store.getters['errorHandler/getLatestError'])

    const clearError = () => {
      store.commit('errorHandler/clearError')
    }

    return {
      latestError,
      clearError,
    }
  }
})
</script>

<style>
.error-box {
  background-color: #800;
  color: white;
  padding: 1em;
  border-radius: 8px;
}
</style>