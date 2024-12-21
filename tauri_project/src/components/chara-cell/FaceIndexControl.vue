<template>
  <div :class="{ highlight: editMode === 'Face' }">
    <button @click="changeCharacter(-1)">-</button>
    <span>Face index (Q,E): {{ faceIndex }}</span>
    <button @click="changeCharacter(1)">+</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const faceIndex = computed(() => store.getters['charaCell/currentBodyState'].faceIndex)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeFaceIndex = (amount: number) => {
      store.commit('charaCell/changeFaceIndex', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Face') {
        return;
      }

      if (event.code === 'KeyQ') {
        changeFaceIndex(-1);
      } else if (event.code === 'KeyE') {
        changeFaceIndex(1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      faceIndex,
      changeCharacter: changeFaceIndex,
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
