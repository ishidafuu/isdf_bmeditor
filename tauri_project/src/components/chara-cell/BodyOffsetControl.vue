<template>
  <div :class="{ highlight: editMode === 'Body' }">
    <div>
      <button @click="changeBodyX(-1)">Move Left</button>
      <span>Body X (A,D): {{ bodyX }}</span>
      <button @click="changeBodyX(1)">Move Right</button>
    </div>
    <div>
      <button @click="changeBodyY(-1)">Move Down</button>
      <span>Body Y (W,S): {{ bodyY }}</span>
      <button @click="changeBodyY(1)">Move Up</button>
    </div>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const bodyX = computed(() => store.getters['charaCell/currentBodyState'].bodyX)
    const bodyY = computed(() => store.getters['charaCell/currentBodyState'].bodyY)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeBodyX = (amount: number) => {
      store.commit('charaCell/changeBodyX', amount)
    }

    const changeBodyY = (amount: number) => {
      store.commit('charaCell/changeBodyY', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Body') {
        return;
      }

      if (event.code === 'KeyA') {
        changeBodyX(-1);
      } else if (event.code === 'KeyD') {
        changeBodyX(1);
      } else if (event.code === 'KeyW') {
        changeBodyY(1);
      } else if (event.code === 'KeyS') {
        changeBodyY(-1);
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown);
    });

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown);
    });

    return {
      bodyX,
      bodyY,
      changeBodyX,
      changeBodyY,
      editMode,
    }
  }
})
</script>

<style scoped>
.highlight {
  color: yellow;
}
</style>
