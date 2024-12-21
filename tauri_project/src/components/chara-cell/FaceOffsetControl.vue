<template>
  <div :class="{ highlight: editMode === 'Face' }">
    <div>
      <button @click="changeFaceX(-1)">Move Left</button>
      <span>Face X (A,D): {{ faceX }}</span>
      <button @click="changeFaceX(1)">Move Right</button>
    </div>
    <div>
      <button @click="changeFaceY(-1)">Move Down</button>
      <span>Face Y (W,S): {{ faceY }}</span>
      <button @click="changeFaceY(1)">Move Up</button>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, computed, onMounted, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const faceX = computed(() => store.getters['charaCell/currentBodyState'].faceX)
    const faceY = computed(() => store.getters['charaCell/currentBodyState'].faceY)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeFaceX = (amount: number) => {
      store.commit('charaCell/changeFaceX', amount)
    }

    const changeFaceY = (amount: number) => {
      store.commit('charaCell/changeFaceY', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Face') {
        return;
      }

      if (event.code === 'KeyA') {
        changeFaceX(-1);
      } else if (event.code === 'KeyD') {
        changeFaceX(1);
      } else if (event.code === 'KeyW') {
        changeFaceY(1);
      } else if (event.code === 'KeyS') {
        changeFaceY(-1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      faceX,
      faceY,
      changeFaceX,
      changeFaceY,
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
