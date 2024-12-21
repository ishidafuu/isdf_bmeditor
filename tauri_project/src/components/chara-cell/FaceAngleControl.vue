<template>
  <div :class="{ highlight: editMode === 'Face' }">
    <button @click="changeCharacter(-1)">-</button>
    <span>Face Angle (R): {{ faceAngle }}</span>
    <button @click="changeCharacter(1)">+</button>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const faceAngle = computed(() => store.getters['charaCell/currentBodyState'].faceAngle)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeCharacter = (amount: number) => {
      store.commit('charaCell/changeFaceAngle', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Face') {
        return;
      }

      if (event.code === 'KeyR') {
        changeCharacter(1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      faceAngle,
      changeCharacter,
      editMode
    }
  }
})
</script>

<style scoped>
.highlight {
  color: yellow;
}
</style>
