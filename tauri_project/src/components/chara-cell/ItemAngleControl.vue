<template>
  <div :class="{ highlight: editMode === 'Item' }">
    <button @click="changeCharacter(-1)">-</button>
    <span>Item Angle (R): {{ itemAngle }}</span>
    <button @click="changeCharacter(1)">+</button>
  </div>
</template>

<script lang="ts">
import {defineComponent, computed, onMounted, onBeforeUnmount} from 'vue'
import {useStore} from 'vuex'

export default defineComponent({
  setup() {
    const store = useStore()

    const itemAngle = computed(() => store.getters['charaCell/currentBodyState'].itemAngle)
    const editMode = computed(() => store.state.charaCell.editMode)

    const changeCharacter = (amount: number) => {
      store.commit('charaCell/changeItemAngle', amount)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        return;
      }

      if (editMode.value !== 'Item') {
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
      itemAngle,
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
